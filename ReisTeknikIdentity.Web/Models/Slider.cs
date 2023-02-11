using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReisTeknikIdentity.Web.Models
{
    public class Slider
    {

        public Slider()
        {
            
        }
        [Key]
        public Guid Id { get; set; }


        [Display(Name ="Başlık")]
        public string?  Title { get; set; }

        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

        [Display(Name="Resim Yolu")]
      
        public string? PathImage { get; set; }

        [NotMapped]
        [Display(Name = "Resim")]       
        public IFormFile ImageFile { get; set; } 
    }
}
