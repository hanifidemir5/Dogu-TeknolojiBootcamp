using BlogApp.Data.Abstract;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers{
    public class PostsController : Controller{
        private IPostRepository _postRepository;
        public PostsController(IPostRepository postRepository){
            _postRepository = postRepository;
        }
        public IActionResult Index(){
            return View(new PostViewModel{
                Posts = _postRepository.Posts.ToList(),
            });
        }

        public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var post = await _postRepository.Posts
                    .FirstOrDefaultAsync(p => p.PostId == id);

                if (post == null)
                {
                    return NotFound();
                }

                return View(post);
            }
        }
};