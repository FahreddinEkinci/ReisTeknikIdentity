using System.ComponentModel.DataAnnotations;

namespace ReisTeknikIdentity.Web.ViewModels
{
    public class ContactViewModel
    {

        public Guid Id { get; set; }

        [Display(Name ="Ad SoyAd")]
        [Required(ErrorMessage ="Ad soyad alana girilmedi")]
        [MaxLength(100,ErrorMessage ="en fazla 100 karakter olabilir")]
        [MinLength(7,ErrorMessage ="e naz 7 karakter olabilir")]
        public string? Name { get; set; }

        [Display(Name = "E-Posta")]
        [Required(ErrorMessage = "E-Posta girilmedi")]
        [MaxLength(60, ErrorMessage = "en fazla 60 karakter olabilir")]
        [MinLength(7, ErrorMessage = "e naz 7 karakter olabilir")]
        [DataType(DataType.EmailAddress,ErrorMessage ="e posta doğru girilmedi")]
        public string? Email { get; set; }


        [Display(Name = "Telefon")]
        [Required(ErrorMessage = "Telefon Numarası girilmedi")]
        [MaxLength(15, ErrorMessage = "en fazla 15 karakter olabilir")]
        [MinLength(10, ErrorMessage = "e naz 10 karakter olabilir")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "telefon numarası doğru girilmedi")]


        public string? Phone { get; set; }
        [Display(Name = "Konu")]
        [Required(ErrorMessage = "Konu girilmedi")]
        [MaxLength(400, ErrorMessage = "en fazla 400 karakter olabilir")]
        [MinLength(20, ErrorMessage = "en az 20 karakter olabilir")]
        public string? Subject { get; set; }

        [Display(Name = "Mesaj")]
        [Required(ErrorMessage = "Mesaj alanı girilmedi")]
        [MaxLength(3000, ErrorMessage = "en fazla 3000 karakter olabilir")]
        [MinLength(50, ErrorMessage = "en az 50 karakter olabilir")]
        public string? Message { get; set; }
    }
}
