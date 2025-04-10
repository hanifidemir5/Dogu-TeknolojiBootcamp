using System.ComponentModel.DataAnnotations;
using BlogApp.Entity;

namespace BlogApp.Models
{
    public class PostEditViewModel
    {
        public int PostId { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? Content { get; set; }
        
        public string? Image { get; set; }

        [Required]
        public string? Url { get; set; }

        public bool IsActive { get; set; }

        public List<int> SelectedTagIds { get; set; } = new List<int>();

        [Display(Name = "Tags")]
        public List<Tag> AllTags { get; set; } = new List<Tag>();

        [Display(Name = "Image")]
        public IFormFile? ImageFile { get; set; }
    }
}
