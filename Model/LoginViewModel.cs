using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models{
    public class LoginViewModel{

        [Required]
        [EmailAddress]
        public string? Email {get;set;}

        [Required]
        [StringLength(20,ErrorMessage = "{0} alanı en az {2} karakter uzunluğunda olmalıdır.",MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string? Password {get;set;}
    }
}