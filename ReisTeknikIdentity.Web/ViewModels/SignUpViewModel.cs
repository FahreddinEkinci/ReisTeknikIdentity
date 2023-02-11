using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReisTeknikIdentity.Web.ViewModels
{
    public class SignUpViewModel
    {
        public SignUpViewModel()
        {

        }

        [Required(ErrorMessage ="{0} giriniz")]
        [Display(Name ="Kullanıcı Adı")]
        public string? UserName { get; set; }

        
        [DataType(DataType.EmailAddress, ErrorMessage ="geçerli bir {0} giriniz")]
        [Required(ErrorMessage = "E-Posta giriniz")]
        [Display(Name = "E-Posta")]

        public string? Email { get; set; }

      
        [DataType(DataType.PhoneNumber, ErrorMessage ="telefon numarası hatalı")]
        [Required(ErrorMessage = "Telefon Numaranızı giriniz")]
        [Display(Name = "Telefon Numarası")]
        public string? PhoneNumber { get; set; }


        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "{0} giriniz")]
        public string?  Password { get; set; }
        [Display(Name = "Şifre Tekrarı")]

        [Compare(nameof(Password), ErrorMessage ="şifreniz aynı değil")]

        [Required(ErrorMessage = "Şifre Tekrar giriniz")]
        public string?  PasswordConfirm { get; set; }


        public string? ProfileImageUrl { get; set; }

        [NotMapped]
        public IFormFile ProfileImage { get; set; }
    }
}
