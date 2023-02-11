using System.ComponentModel.DataAnnotations;

namespace ReisTeknikIdentity.Web.ViewModels
{
    public class ChangeUserNameViewModel
    {
        [Display(Name = "Kullanıcı Adı")]
        [MinLength(4, ErrorMessage = "kullanıcı adı en az 4 karakter olmalıdır")]
        [MaxLength(50, ErrorMessage = "kullanıcı adı en fazla 50 karakter olmalıdır")]
        [Required(ErrorMessage ="{0} girmediniz")]
        public string UserName { get; set; } 

    }
}
