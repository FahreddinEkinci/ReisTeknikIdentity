using System.ComponentModel.DataAnnotations.Schema;

namespace ReisTeknikIdentity.Web.Models
{
    public class Logo
    {

        public int Id { get; set; } 

        public string? Name { get; set; }

        [NotMapped]
        public IFormFile LogoFile { get; set; }
    }
}
