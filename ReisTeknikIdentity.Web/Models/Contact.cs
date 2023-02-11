using System.ComponentModel.DataAnnotations;

namespace ReisTeknikIdentity.Web.Models
{
    public class Contact
    {

        [Key]
        public Guid Id { get; set; }       
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Subject { get; set; }
        public string? Message { get; set; }
    }
}
