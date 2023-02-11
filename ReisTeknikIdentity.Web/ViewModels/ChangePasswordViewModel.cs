using System.ComponentModel.DataAnnotations;

namespace ReisTeknikIdentity.Web.ViewModels
{
    public class ChangePasswordViewModel
    {
        internal Guid Id { get; set; }

        [Display(Name = "Mevcut Şifre")]
        public string PasswordOld { get; set; }


        [Display(Name = "Şifre Tekrarı")]
        [Required(ErrorMessage = "Şifre Tekrar giriniz")]
        public string NewPassword { get; set; }


        [Display(Name = "Şifre Tekrarı")]
        [Required(ErrorMessage = "Şifre Tekrar giriniz")]
        [DataType(DataType.Password)]

        [Compare(nameof(NewPassword), ErrorMessage = "şifreniz aynı değil")]
        public string NewPasswordConfirm { get; set; }
    }
}
