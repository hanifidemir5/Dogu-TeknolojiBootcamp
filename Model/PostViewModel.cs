using BlogApp.Entity;

namespace BlogApp.Models{
    public class PostViewModel{
    public List<Post> Posts { get; set; } = new();
    public List<List<Tag>> Tags { get; set; } = new();  // This will hold tags for each post
}

}