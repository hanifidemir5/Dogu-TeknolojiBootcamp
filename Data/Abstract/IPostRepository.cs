using BlogApp.Entity;

namespace BlogApp.Data.Abstract{
    public interface IPostRepository{
        IQueryable<Post> Posts { get; }
        Task CreatePostAsync(Post post);
        Task EditPostAsync(Post post);
        void DeletePost(Post post);
        Task<IEnumerable<Post>> GetPostsByCategoryAsync(string categoryUrl);
    }
}