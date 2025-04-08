using BlogApp.Entity;

namespace BlogApp.Data.Abstract{
    public interface ITagRepository{
        IQueryable<Tag> Tags { get; }
        IQueryable<Tag> GetTagsByIds(List<int> tagIds);
        void CreateTag(Tag Tag);
    }
}