using System.ComponentModel.DataAnnotations.Schema;

namespace ReisTeknikIdentity.Web.Models
{
    public class BaseEntity
    {
        
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }
    }
}
