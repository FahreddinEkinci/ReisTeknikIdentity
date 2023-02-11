using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReisTeknikIdentity.Web.IdentityModels;
using ReisTeknikIdentity.Web.ViewModels;

namespace ReisTeknikIdentity.Web.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public MemberController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
          var  currentUser =await  _userManager.FindByNameAsync(User.Identity!.Name!);

            Console.WriteLine(currentUser.Id);

            var us = new MemberViewModel()
            {
                Id = Guid.Parse(currentUser.Id),
                Name = currentUser!.UserName!,
                Email = currentUser.Email!,
                PhoneNumber = currentUser.PhoneNumber
            };

            return View(us);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
            var hasUser = await _userManager.FindByIdAsync(userId);
            if (hasUser == null)
            {
                return NotFound();
            }

            ChangePasswordViewModel md = new ()
            {

                Id = Guid.Parse(hasUser.Id)
            };

            return View(md);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ChangePasswordViewModel model, string Id)
        {

            var hasUser = await _userManager.FindByIdAsync(Id);
            if (hasUser == null)
            {
                return NotFound();
            }

            var checkPassword = await _userManager.CheckPasswordAsync(hasUser,model.PasswordOld);
            if (checkPassword == false)
            {
                ModelState.AddModelError(string.Empty, "Eski şifrenizi hatalı girdiniz");
                return View(model);
            }
            IdentityResult result = await _userManager.ChangePasswordAsync(hasUser, model.PasswordOld, model.NewPasswordConfirm);

            if (result.Succeeded)
            {

                return RedirectToAction(nameof(Index),"Home");

            }
            else
            {
                foreach (var error in result.Errors)
                {

                ModelState.AddModelError(string.Empty, error.Description);
                }
                return View();
            }
        }

        [HttpPost]  // action change user name
        public async Task<IActionResult> ChangeUserName(ChangeUserNameViewModel model)
        {
            if (ModelState.IsValid)
            {
                var hasUser =await  _userManager.FindByNameAsync(User.Identity.Name);

                if (hasUser ==null)
                {
                    return BadRequest();
                }

                hasUser.UserName = model.UserName;
               


                await _userManager.UpdateAsync(hasUser!);
            }
            return View();
        }
    }
}
