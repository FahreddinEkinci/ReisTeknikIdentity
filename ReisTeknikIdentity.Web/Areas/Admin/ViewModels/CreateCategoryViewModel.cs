using System.ComponentModel.DataAnnotations;

namespace ReisTeknikIdentity.Web.Areas.Admin.ViewModels
{
    public class CreateCategoryViewModel
    {

        [Required(ErrorMessage ="{0} girilmedi")]
        [Display(Name ="Kategori Adı")]
        public string Name { get; set; } = null!;

        
        [Display(Name = "Kategori Açıklama")]
        public string? Description { get; set; }
    }
}
