using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReisTeknikIdentity.Web.Areas.Admin.ViewModels
{
    public class UserViewModel
    {

        public string? Id { get; set; }


      //  [Required(ErrorMessage = "{0} Alanı boş bırakılamaz")]
        [Display(Name = "Kullanıcı Adı")]
        public string? UserName { get; set; }

        [EmailAddress(ErrorMessage = "E Posta formatı hatalı")]
     //   [Required(ErrorMessage = "E-Posta Alanı boş bırakılamaz")]
        [Display(Name = "E-Posta")]

        public string? Email { get; set; }

        [Phone(ErrorMessage = "Telefon Formatı Hatalı")]
      //  [Required(ErrorMessage = "Telefon Numarası Alanı boş bırakılamaz")]
        [Display(Name = "Telefon Numarası")]

        public string? PhoneNumber { get; set; }

        [Display(Name = "Şifre")]

        //[Required(ErrorMessage = "Şifre Alanı boş bırakılamaz")]
        public string? Password { get; set; }
        

        [Display(Name = "Resim")]

        public string? ProfileImageUrl { get; set; }


        [Display(Name ="Kayıt Tarihi")]
        public DateTime? CreatedAt { get; set; }

        [Display(Name ="Durum")]
        public string? Status { get; set; }


    }
}
