using System.ComponentModel.DataAnnotations;

namespace ReisTeknikIdentity.Web.Areas.Admin.ViewModels
{
    public class RoleViewModel
    {

        public string? Id { get; set; }

        [Display(Name = "Adı")]
        [Required(ErrorMessage = "{0} girilmedi")]
        public string? Name { get; set; }
    }
}
