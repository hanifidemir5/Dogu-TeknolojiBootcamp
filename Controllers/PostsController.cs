using BlogApp.Data.Abstract;
using BlogApp.Models;
using BlogApp.Entity;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;

namespace BlogApp.Controllers{
    public class PostsController : Controller{
        private IPostRepository _postRepository;
        private ICommentRepository _commentRepository;
        private ITagRepository _tagRepository;

        public PostsController(IPostRepository postRepository, ICommentRepository commentRepository, ITagRepository tagRepository){
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _tagRepository = tagRepository;
        }

        // GET: Index
        public async Task<IActionResult> Index(string tag, int page = 1, int pageSize = 9)
        {
            var postsQuery = _postRepository.Posts
                .Include(p => p.Tags)
                .Where(x => x.IsActive);

            if (!string.IsNullOrEmpty(tag))
            {
                postsQuery = postsQuery.Where(x => x.Tags.Any(t => t.Url == tag));
            }

            var totalPosts = await postsQuery.CountAsync();

            var postList = await postsQuery
                .OrderByDescending(p => p.PublishedOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var postTags = postList.ToDictionary(
                post => post.PostId,
                post => post.Tags.ToList()
            );

            var viewModel = new PostViewModel
            {
                Posts = postList,
                PostTags = postTags,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalPosts / (double)pageSize)
            };

            return View(viewModel);
        }


        // GET: Details
        public async Task<IActionResult> Details(string url){
            var post = await _postRepository.Posts
                            .Include(x => x.Tags)
                            .Include(x => x.Comments)
                            .ThenInclude(x => x.User)
                            .FirstOrDefaultAsync(p => p.Url == url);
            return View(post);
        }

        // POST: Add Comment
        [HttpPost]
        public JsonResult AddComment(int PostId, string Text){
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = User.FindFirstValue(ClaimTypes.Name);
            var avatar = User.FindFirstValue(ClaimTypes.UserData);

            if (string.IsNullOrEmpty(userId)){
                return Json(new { success = false, message = "User is not authenticated" });
            }

            var entity = new Comment{
                Text = Text,
                PublishedOn = DateTime.Now,
                PostId = PostId,
                UserId = int.Parse(userId)
            };

            _commentRepository.CreateComment(entity);

            return Json(new {
                username,
                Text,
                PublishedOn = entity.PublishedOn.ToString(),
                avatar
            });
        }

        // GET: Create Post
        [Authorize]
        public async Task<IActionResult> Create(){
            var allTags = await _tagRepository.Tags.ToListAsync();
            var viewModel = new PostCreateViewModel{
                AllTags = allTags
            };
            return View(viewModel);
        }

        // POST: Create Post
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(PostCreateViewModel model){
            if (ModelState.IsValid){
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null){
                    return Unauthorized();
                }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img");
                Directory.CreateDirectory(uploadsFolder);
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create)){
                    await model.ImageFile.CopyToAsync(stream);
                }
                
                var post = new Post{
                    Title = model.Title,
                    Content = model.Content,
                    Url = model.Url,
                    UserId = int.Parse(userId),
                    PublishedOn = DateTime.Now,
                    Description = model.Description,
                    Image = fileName,
                    IsActive = false
                };

                await _postRepository.CreatePostAsync(post);

                post.Tags = post.Tags ?? new List<Tag>();

                if (model.SelectedTagIds != null && model.SelectedTagIds.Any()){
                    var selectedTags = await _tagRepository.GetTagsByIds(model.SelectedTagIds).ToListAsync();
                    post.Tags.AddRange(selectedTags);
                }

                await _postRepository.EditPostAsync(post);

                return RedirectToAction("Index");
            }

            model.AllTags = _tagRepository.Tags.ToList();
            return View(model);
        }

        // GET: List of Posts
        [Authorize]
        public async Task<IActionResult> List(){
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
            var role = User.FindFirstValue(ClaimTypes.Role);

            var posts = _postRepository.Posts;

            if (string.IsNullOrEmpty(role)){
                posts = posts.Where(i => i.UserId == userId);
            }

            return View(await posts.ToListAsync());
        }

        // GET: Edit Post
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int? id){
            if (id == null){
                return NotFound();
            }

            var post = await _postRepository.Posts.Include(p => p.Tags).FirstOrDefaultAsync(i => i.PostId == id);
            if (post == null){
                return NotFound();
            }

            var viewModel = new PostEditViewModel{
                PostId = post.PostId,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
                Url = post.Url,
                ImageFile = null, 
                Image = post.Image, 
                IsActive = post.IsActive,
                SelectedTagIds = post.Tags.Select(t => t.TagId).ToList(),
                AllTags = await _tagRepository.Tags.ToListAsync()
            };

            return View(viewModel);
        }

        // POST: Edit Post
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(PostEditViewModel model){
            if (ModelState.IsValid){
                var entityToUpdate = await _postRepository.Posts
                .Include(p => p.Tags).FirstOrDefaultAsync(p => p.PostId == model.PostId);

                if (entityToUpdate == null)
                {
                    return NotFound();
                }

                entityToUpdate.Title = model.Title;
                entityToUpdate.Description = model.Description;
                entityToUpdate.Content = model.Content;
                entityToUpdate.Url = model.Url;
                var currentTagIds = entityToUpdate.Tags.Select(t => t.TagId).ToList();

                var selectedTagIds = model.SelectedTagIds;

                if (!currentTagIds.SequenceEqual(selectedTagIds))
                {
                    entityToUpdate.Tags.Clear();

                    var selectedTags = await _tagRepository.GetTagsByIds(selectedTagIds).ToListAsync();
                    entityToUpdate.Tags.AddRange(selectedTags);
                }

                if (model.ImageFile != null)
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", entityToUpdate.Image);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img");
                    var newFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                    var newFilePath = Path.Combine(uploadsFolder, newFileName);

                    using (var stream = new FileStream(newFilePath, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(stream);
                    }

                    entityToUpdate.Image = newFileName;
                }

                if (User.FindFirstValue(ClaimTypes.Role) == "admin"){
                    Console.WriteLine("Role: " + User.FindFirstValue(ClaimTypes.Role));
                    Console.WriteLine("IsActive: " + model.IsActive);
                    entityToUpdate.IsActive = model.IsActive;
                }

                await _postRepository.EditPostAsync(entityToUpdate);

                return RedirectToAction("List");
            }
            else{
                Console.WriteLine("Model state is invalid.");
            }
            return View(model);
        }
    }
}
