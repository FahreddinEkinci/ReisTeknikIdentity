using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using ReisTeknikIdentity.Web.Areas.Admin.ViewModels;
using ReisTeknikIdentity.Web.Data;
using ReisTeknikIdentity.Web.IdentityModels;
using ReisTeknikIdentity.Web.Servicess;
using ReisTeknikIdentity.Web.ViewModels;

namespace ReisTeknikIdentity.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;



        public UserController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, AppDbContext context, IEmailService emailService)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(UserViewModel userModel) { return Ok(); }

        [HttpGet]
        public IActionResult SignIn()
        {
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel userModel, string? returnUrl = null)
        {
            returnUrl = returnUrl
                ?? Url.Action("Index", "Home");

            if (userModel.Email == null || userModel.Password == null)
            {
                ModelState.AddModelError(string.Empty, $"giriş bilgilerinizi kontrol ediniz.");

                return View(userModel);
            }

            if (ModelState.IsValid)
            {
                var hasUser = await _userManager.FindByEmailAsync(userModel.Email);
                if (hasUser == null || string.IsNullOrEmpty(userModel.Password))
                {
                    ModelState.AddModelError(string.Empty, "E-Posta yada şifre yanlış");
                    return View(userModel);

                }

                var signInresult = await _signInManager.PasswordSignInAsync(hasUser, userModel.Password, userModel.RememberMe, true);
                if (signInresult.Succeeded)
                {
                    return Redirect(returnUrl!);
                }

                if (!signInresult.Succeeded || hasUser != null)
                {
                    ModelState.AddModelError(string.Empty, $"kullanıcı adı yada şifre hatalı " + $" başarısız deneme sayısı : {await _userManager.GetAccessFailedCountAsync(hasUser)}");
                    return View(userModel);

                }
                if (!signInresult.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, $"çok fazla deneme yaptınız.. 5 dakika sonra yeniden deneyiniz");
                    return View(userModel);
                }

            }



            ModelState.AddModelError(string.Empty, $"giriş bilgilerinizi kontrol ediniz.");

            return View(userModel);
        }


        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IActionResult> LogOut()
        {

            await _signInManager.SignOutAsync();
          return   RedirectToAction("Index","Home");
        }

        public IActionResult ForgetPassword()
        {
            TempData["TypeAlert"] = "success";
            TempData["SuccessMessage"] = "şifreniz başarı ile değiştirildi.";

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel request)
        {

            var hasUser = await _context.Users.Where(x => x.Email == request.Email).FirstOrDefaultAsync();

            if (hasUser == null)
            {
                // kayıtlı postası yok biz bu hatayı diğer üyelerin güvenliği için böyle dönüyoruz. 
                TempData["Success"] = "Posta Kutunuzu kontrol edin";
                ModelState.AddModelError(string.Empty, "Kayıtlı hesabınız varsa şifre yenileme link posta kutunuza gönderilecektir.");
                return View(request);
            }

            if (hasUser != null)
            {

                var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(hasUser);
                var passwordResetLink = Url.Action("ResetPassword", "User", new { userId = hasUser.Id, token = passwordResetToken }, HttpContext.Request.Scheme);

                await _emailService.SendResetPasswordEmail(passwordResetLink!, request.Email!);

                TempData["Success"] = "Posta Kutunuzu kontrol edin";
                return View();

            }

            ModelState.AddModelError(string.Empty, "hata var");
            return View(request);



        }


        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            TempData["userId"] = userId;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel request)
        {

            var userId = TempData["userId"];
            var token = TempData["token"]; 

            if(userId == null || token == null)
            {
                
                return Content(HttpContext.Connection.RemoteIpAddress.ToString(), "ip adresinden gezmemen gereken yerlerde geziyorsun.HACI YAPMA BÖYLE AYIP OLUR. ");
            }

            var hasUser = await _userManager.FindByIdAsync(userId.ToString());
            if (hasUser ==null)
            {
                return BadRequest();
            }

            IdentityResult result = await _userManager.ResetPasswordAsync(hasUser, token.ToString(), request.PasswordConfirm);

            if (result.Succeeded)
            {
                TempData["TypeAlert"] = "success";
                TempData["SuccessMessage"] = "şifreniz başarı ile değiştirildi.";
                return RedirectToAction("SignIn", "User");
            }else
            {
                ModelState.AddModelError(string.Empty, result.Errors.Select(x=> x.Description).ToString());
                return View(result);

            }
        }


    }
}
