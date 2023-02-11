using System.ComponentModel.DataAnnotations;

namespace ReisTeknikIdentity.Web.Areas.Admin.ViewModels
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
                
        }
        [Display(Name ="Kategori Id")]
        public Guid Id { get; set; }

        [Required(ErrorMessage ="{0} girilmedi")]
        [Display(Name ="Kategori Adı")]
        public string Name { get; set; } = null!;

        
        [Display(Name = "Kategori Açıklama")]
        public string? Description { get; set; }
        [Display(Name = "Eklenme Tarihi")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Eklenme Güncellenme Tarihi")]
        public DateTime UpdatedDate { get; set; }


    }
}
