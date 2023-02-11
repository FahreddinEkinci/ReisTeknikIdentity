using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReisTeknikIdentity.Web.Areas.Admin.ViewModels;
using ReisTeknikIdentity.Web.IdentityModels;

namespace ReisTeknikIdentity.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public HomeController(UserManager<AppUser> userManager)
        {
            _userManager= userManager;  
        }
      

        [HttpGet]
        public IActionResult Index()
        {

           
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            var userList = await _userManager.Users.ToListAsync();

            var userListByViewModel = userList.Select(x => new UserViewModel()
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                ProfileImageUrl = x.ProfileImageUrl,

            }).ToList();

            return View(userListByViewModel);
        }



    }
}
