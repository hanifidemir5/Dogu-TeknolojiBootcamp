using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;
using BlogApp.Data.Abstract;
using System.Linq;


namespace BlogApp.Data.Concrete.EfCore{
    public class EfCategoryRepository : ICategoryRepository{
        private readonly BlogContext _context;

        public EfCategoryRepository(BlogContext context){
            _context = context;
        }

        public IQueryable<Category> Categories => _context.Categories.Include(c => c.Posts);

        public async Task CreateCategoryAsync(Category category){
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }
    }
}