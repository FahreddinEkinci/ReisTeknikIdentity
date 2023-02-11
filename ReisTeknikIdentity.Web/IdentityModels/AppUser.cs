using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReisTeknikIdentity.Web.IdentityModels
{
    public class AppUser:IdentityUser
    {
      
        public string? ProfileImageUrl { get; set; }

        [NotMapped]
        public IFormFile ProfileImage { get; set; }


        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        [NotMapped]
        public string Status { get; set; }


    }
}
