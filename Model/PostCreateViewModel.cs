using System.ComponentModel.DataAnnotations;
 using BlogApp.Entity;
 
namespace BlogApp.Models{
    public class PostBaseViewModel
    {
        public int PostId { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required, MaxLength(32)]
        public string? Description { get; set; }

        [Required]
        public string? Content { get; set; }

        public string? Image { get; set; }

        [Required]
        public string? Url { get; set; }

        public bool IsActive { get; set; }

        public List<int> SelectedTagIds { get; set; } = new();
        
        [Display(Name = "Tags")]
        public List<Tag> AllTags { get; set; } = new();
    }

    public class PostCreateViewModel : PostBaseViewModel
    {
        [Display(Name = "Image")]
        [Required(ErrorMessage = "Image is required when creating a post.")]
        public IFormFile ImageFile { get; set; }
    }

    public class PostEditViewModel : PostBaseViewModel
    {
        public IFormFile? ImageFile { get; set; } // Optional on edit
    }

}
