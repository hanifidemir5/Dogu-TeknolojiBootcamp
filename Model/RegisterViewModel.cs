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
        [StringLength(10,ErrorMessage = "{0} alanı en az {2} karakter uzunluğunda olmalıdır.",MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string? Password {get;set;}

        [Display(Name = "Image")]
        [Required(ErrorMessage = "Image is required.")]
        public IFormFile ImageFile { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage = "Parolanız eşleşmiyor.")]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword {get;set;}
    }
}