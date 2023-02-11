using System.ComponentModel.DataAnnotations;

namespace ReisTeknikIdentity.Web.ViewModels
{
    public sealed class CategoryListComponentViewModel
    {
        public CategoryListComponentViewModel()
        {

        }

        [Display(Name = "Benzersiz Kod")]
        public string Id { get; set; }

        [Display(Name ="Adı")]
        public string? Name { get; set; }
    }
}
