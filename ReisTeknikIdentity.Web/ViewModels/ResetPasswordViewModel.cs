namespace ReisTeknikIdentity.Web.ViewModels
{

    using System.ComponentModel.DataAnnotations;

    public class ResetPasswordViewModel
    {

        public ResetPasswordViewModel()
        {

        }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "{0} giriniz")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }


        [Display(Name = "Şifre Tekrarı")]
        [Required(ErrorMessage = "Şifre Tekrar giriniz")]
        [DataType(DataType.Password)]

        [Compare(nameof(Password), ErrorMessage = "şifreniz aynı değil")]
        public string? PasswordConfirm { get; set; }

    }
}
