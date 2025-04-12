using BlogApp.Entity;

namespace BlogApp.Models{
    public class PostViewModel
    {
        public List<Post>? Posts { get; set; }
        public Dictionary<int, List<Tag>>? PostTags { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }

}