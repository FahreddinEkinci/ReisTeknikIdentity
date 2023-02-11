

using System.ComponentModel.DataAnnotations;

namespace ReisTeknikIdentity.Web.ViewModels
{
    public class SignInViewModel
    {
       

        [Required(ErrorMessage = "E-Posta giriniz")]
        [Display(Name = "E-Posta")]
        public string? Email { get; set; }


        [Required(ErrorMessage ="Şifrenizi giriniz")]
        [Display(Name ="Şifre")]
        public string? Password { get; set; }

        [Display(Name ="Beni hatırla ?")]
        public bool RememberMe { get; set; }
    }
}
