using ReisTeknikIdentity.Web.Models;

namespace ReisTeknikIdentity.Web.Areas.Admin.ViewModels
{
    public class CreateProductViewModel
    {


        public string? Name { get; set; }


        public string? Description { get; set; }


        public string? Title { get; set; }

        public string? Brand { get; set; }


        public decimal Price { get; set; }

        public int Stock { get; set; }

        public bool State { get; set; }

        public string CategoryId { get; set; }

        public virtual ICollection<Image>? Images { get; set; }

        public virtual ICollection<Category>? Categories { get; set; }
    }
}
