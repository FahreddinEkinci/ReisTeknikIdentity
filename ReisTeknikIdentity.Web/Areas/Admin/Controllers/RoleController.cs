using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReisTeknikIdentity.Web.Areas.Admin.ViewModels;
using ReisTeknikIdentity.Web.Data;
using ReisTeknikIdentity.Web.IdentityModels;
using System.Data;

namespace ReisTeknikIdentity.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {

        private readonly RoleManager<AppRole> _roleManager;
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManeger;

        public RoleController(RoleManager<AppRole> roleManager, AppDbContext context, UserManager<AppUser> userManeger)
        {
            _roleManager = roleManager;
            _context = context;
            _userManeger = userManeger;
        }

        public async Task<IActionResult> Index()
        {

            //return _context.Roles != null
            //    ? View(await _roleManager.Roles.ToListAsync()) :
            //    Problem("kullanıcı yetki gurubu boş veri getirilemedi");

            TempData["success"] = "";

            if (_roleManager.Roles != null)
            {
                return View(await _roleManager.Roles.Select(x => new RoleViewModel{ Id =x.Id, Name = x.Name }).ToListAsync());
            }

            return Content("yetki grubu boş");

        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new AppRole();
                role.Name = model.Name;
                IdentityResult result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    TempData["Success"] = "Yetki grubu başarı ile eklendi";

                    return RedirectToAction(nameof(RoleController.Index));
                }

                result.Errors.ToList().ForEach(x => ModelState.AddModelError(string.Empty, x.Description));
            }
            ModelState.AddModelError("", "rol eklenemedi bir hata oluştu");

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Update(string Id)
        {
            var role = await _roleManager.FindByNameAsync(Id);

            if (role == null)
            {

                return NotFound();
            }
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(RoleViewModel model)
        {
            var role = await _roleManager.FindByNameAsync(model.Id);

            role.Name = model.Name;

            IdentityResult result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                TempData["success"] = "Yetki adı başarı ile değiştirildi.";
                return View(nameof(Index));
            }

            return View(role);

        }


        [HttpGet]
        public async Task<IActionResult> AddRoleToUser()
        {

            var roleUser = new UserRoleViewModel();


            var roles = await _roleManager.Roles.ToListAsync();
            var users = await _context.Users.ToListAsync();

            ViewBag.Roles = new SelectList(roles, "Name", "Name");
            ViewBag.Users = new SelectList(users, "Id", "UserName");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRoleToUser(UserRoleViewModel model)
        {
            //var rr = new UserRoleViewModel() {  RoleName = model.RoleName, UserId = model.UserId};

            var user = await _userManeger.FindByIdAsync(model.UserId);

            var role = new AppRole { Name = model.RoleName };
            var result = await _userManeger.AddToRoleAsync(user, role.Name);



            if (result.Succeeded)
            {
                TempData["success"] = "yetki başarı ile atandı";
                return RedirectToAction("Index");
            }
            result.Errors.ToList().ForEach(err => ModelState.AddModelError(string.Empty, err.Description));


            return View(model);
        }


    }
}
