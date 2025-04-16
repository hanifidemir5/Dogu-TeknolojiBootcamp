using System.ComponentModel.DataAnnotations;
 using BlogApp.Entity;
 
namespace BlogApp.Models{
    public class PostBaseViewModel
    {
        public int PostId { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required, MaxLength(100)]
        public string? Description { get; set; }
        
             
        [Display(Name = "Categories")]
        public List<Category> AllCategories { get; set; } = new();

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        public string? Content { get; set; }

        public string? Image { get; set; }

        [Required]
        public string? Url { get; set; }

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "At least one tag must be selected.")]
        [MinLength(1, ErrorMessage = "At least one tag must be selected.")]
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
        public IFormFile? ImageFile { get; set; }
    }

}
