using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BlogApp.Data.Concrete.EfCore
{
    public class EfTagRepository : ITagRepository
    {
        private readonly BlogContext _context;

        public EfTagRepository(BlogContext context)
        {
            _context = context;
        }

        public IQueryable<Tag> Tags => _context.Tags;

        public void CreateTag(Tag Tag)
        {
            _context.Tags.Add(Tag);
            _context.SaveChanges();        
        }
    }
}
