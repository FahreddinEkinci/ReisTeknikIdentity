using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReisTeknikIdentity.Web.Models
{
    public class Product : BaseEntity
    {

        public Product()
        {
            Images = new List<Image>();
        }

        [Display(Name ="Ürün Adı")]
        public string? Name { get; set; }

        [Display(Name = "Ürün Açıklama")]
        public string? Description { get; set; }

        [Display(Name = "Ürün Başlık")]
        public string? Title { get; set; }
        [Display(Name = "Ürün Marka")]
        public string?  Brand { get; set; }

        [Display(Name = "Ürün Fiyatt")]
        [Precision(18,2)]
        public decimal Price { get; set; }

        [Display(Name = "Stok")]
        public int Stock { get; set; }
        [Display(Name = "Ürün Durumu")]
        public bool State { get; set; }

        [NotMapped]
        public IFormFile[] Files { get; set; }

        public List<Image>? Images { get; set; }

        [Display(Name ="Kategori")]
        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
    }
}
