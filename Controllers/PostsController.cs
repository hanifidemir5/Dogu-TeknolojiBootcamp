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
        public async Task<IActionResult> Index(string tag){
            var posts = _postRepository.Posts;

            if (!string.IsNullOrEmpty(tag)){
                posts = posts.Where(x => x.Tags.Any(t => t.Url == tag));
            }

            var postList = await posts.ToListAsync();
            var tagsList = postList.Select(post => post.Tags.ToList()).ToList();

            var viewModel = new PostViewModel{
                Posts = postList,
                Tags = tagsList
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
                    Image = "/img/" + fileName,
                    IsActive = false
                };

                await _postRepository.CreatePostAsync(post);

                if (model.SelectedTagIds != null && model.SelectedTagIds.Any()){
                    var selectedTags = _tagRepository.GetTagsByIds(model.SelectedTagIds).ToList();
                    post.Tags.AddRange(selectedTags);
                }

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
        [Authorize]
        public async Task<IActionResult> Edit(int? id){
            if (id == null){
                return NotFound();
            }

            var post = await _postRepository.Posts.Include(p => p.Tags).FirstOrDefaultAsync(i => i.PostId == id);
            if (post == null){
                return NotFound();
            }

            var viewModel = new PostCreateViewModel{
                PostId = post.PostId,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
                Url = post.Url,
                ImageFile = null, 
                IsActive = post.IsActive,
                SelectedTagIds = post.Tags.Select(t => t.TagId).ToList(),
                AllTags = await _tagRepository.Tags.ToListAsync()
            };

            return View(viewModel);
        }

        // POST: Edit Post
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(PostCreateViewModel model){
            if (ModelState.IsValid){
                // var entityToUpdate = await _postRepository.Posts
                // .Include(p => p.Tags)
                // .FirstOrDefaultAsync(p => p.PostId == model.PostId);

                var entityToUpdate = new Post{
                    PostId = model.PostId,
                    Title = model.Title,
                    Description = model.Description,
                    Content = model.Content,
                    Url = model.Url
                };
                Console.WriteLine("Post Edit Payload:");
                Console.WriteLine($"PostId: {model.PostId}");
                Console.WriteLine($"Title: {model.Title}");
                Console.WriteLine($"Description: {model.Description}");
                Console.WriteLine($"Content: {model.Content}");
                Console.WriteLine($"Url: {model.Url}");

                // if (User.FindFirstValue(ClaimTypes.Role) == "admin"){
                //     entityToUpdate.IsActive = model.IsActive;
                // }

                // await _postRepository.EditPostAsync(entityToUpdate);
                // return RedirectToAction("List");
            }
            return View(model);
        }
    }
}
