namespace ReisTeknikIdentity.Web.ViewModels
{

    using System.ComponentModel.DataAnnotations;

    public class ForgetPasswordViewModel
    {

        public ForgetPasswordViewModel() { }    


        [Required(ErrorMessage="E Postanızı giriniz")]
        [DataType(DataType.Password)]
        [Display(Name ="E-Posta")]
        public string? Email { get; set; }
    }
}
