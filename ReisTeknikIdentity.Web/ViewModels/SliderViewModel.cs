
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReisTeknikIdentity.Web.ViewModels
{
    
    public class SliderViewModel
    {


        [Display(Name = "Başlık")]
        public string? Title { get; set; }

        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
        [Display(Name = "Resim Yolu")]

        public string? PathImage { get; set; }

         [Display(Name = "Resim")]
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
