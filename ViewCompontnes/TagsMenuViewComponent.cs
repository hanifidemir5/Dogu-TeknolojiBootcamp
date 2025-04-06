using BlogApp.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.ViewComponents{
    public class TagsMenuViewComponent:ViewComponent{
        private ITagRepository _tagRepository;
        public TagsMenuViewComponent(ITagRepository tagRepository){
            _tagRepository = tagRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(){
            var tags = await _tagRepository.Tags.ToListAsync();
            return View(tags);        
        }
    }
}