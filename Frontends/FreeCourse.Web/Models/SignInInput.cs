using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models
{
    public class SignInInput
    {
        [Required]
        [Display(Name = "Email Adresiniz..")]
        public string Email { get; set; } = null!;

        [Required]
        [Display(Name = "Şifrenizi Giriniz..")]
        public string Password { get; set; } = null!;

        [Display(Name = "Beni Hatırla..")]
        public bool IsRemember { get; set; }
    }
}
