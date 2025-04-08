using BlogApp.Data.Abstract;
using BlogApp.Models;
using BlogApp.Entity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers{
    public class PostsController : Controller{
        private IPostRepository _postRepository;
        private ICommentRepository _commentRepository;
        public PostsController(IPostRepository postRepository, ICommentRepository commentRepository){
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }
        public async Task<IActionResult> Index(string tag){
            var claims = User.Claims;
            var posts = _postRepository.Posts;

            if(!string.IsNullOrEmpty(tag)){
                posts = posts.Where(x=>x.Tags.Any(t=>t.Url == tag));
            }
            return View(new PostViewModel{Posts = await posts.ToListAsync()});
         }

        public async Task<IActionResult> Details(string url)
            {
                return View(await _postRepository.Posts.Include(x=>x.Tags).Include(x=>x.Comments).ThenInclude(x=>x.User).FirstOrDefaultAsync(p=>p.Url == url));
            }
        }
};