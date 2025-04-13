using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace BlogApp.Models{
    public class EditProfileViewModel
    {
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string? UserName { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string? Name { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public string? Image { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0} must be between 6 and 20 characters")]
        public string? NewPassword { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}