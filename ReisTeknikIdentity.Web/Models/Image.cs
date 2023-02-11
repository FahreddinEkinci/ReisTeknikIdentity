using System.ComponentModel.DataAnnotations.Schema;

namespace ReisTeknikIdentity.Web.Models
{
    public class Image : BaseEntity
    {

        public string? Name { get; set; }


       // public String? ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
