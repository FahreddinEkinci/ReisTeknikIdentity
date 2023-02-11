using System.ComponentModel.DataAnnotations;

namespace ReisTeknikIdentity.Web.Areas.Admin.ViewModels
{
    public class UserRoleViewModel
    {

        public string UserId { get; set; }

        [Display(Name ="Yetki Adı")]
        public string? RoleName { get; set; }

        public List<UserViewModel> Users { get; set; }

        public List<string> RoleNames  { get; set; }
    }
}
