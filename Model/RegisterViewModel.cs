using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models{
    public class RegisterViewModel{

        [Required]
        [Display(Name = "Username")]
        public string? Username {get;set;}
        [Required]
        [Display(Name = "Name")]
        public string? Name {get;set;}
        [Required]
        [EmailAddress]
        public string? Email {get;set;}

        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0} must be between 6 and 20 characters")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "Image is required.")]
        public required IFormFile ImageFile { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage = "Passwords do not match.")]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword {get;set;}
    }
}