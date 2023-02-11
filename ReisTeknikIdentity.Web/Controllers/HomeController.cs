using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReisTeknikIdentity.Web.Data;
using ReisTeknikIdentity.Web.IdentityModels;
using ReisTeknikIdentity.Web.Models;
using ReisTeknikIdentity.Web.ViewModels;
using System.Diagnostics;


namespace ReisTeknikIdentity.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


          [Route("/anasayfa")]
             [Route("/")]
       
        public async Task<IActionResult> Index()
        {

            var products = await _context.Products
                .Include(img => img.Images)
                .ToListAsync();

            return View(products);
        }

        [Route("/kategori/{Id}")]
        public async Task<IActionResult> GetProductsByCategory(string Id, string name)
        {       
           
            

            var ctg = await _context.Categories
                .Include(p => p.Products)
                .ThenInclude(img => img.Images)
                .Where(ct => ct.Id == Guid.Parse(Id)).FirstOrDefaultAsync();

            var produts = await _context.Products.Include(img => img.Images).Include(ct => ct.Category)
               .Where(p => p.CategoryId == Guid.Parse(Id)).ToArrayAsync();


            if (produts != null)
            {
                return View("Index", produts);
            }

            return View("Index");
        }


        [HttpGet]
        public IActionResult SignUp()
        {
            TempData["success"] = "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            TempData["success"] = "";

            var IsThereUserEmail = _context.Users.Where(x => x.Email == model.Email.ToLower()).FirstOrDefault();
            if (IsThereUserEmail != null)
            {
                var i = new IdentityError { Code = "IsThereEmail", Description = "e posta daha önce kayıt edildi" };
                ModelState.AddModelError(string.Empty, i.Description);
                return View(model);
            }

            if (ModelState.IsValid)
            {
                var IdentityResult = await _userManager.CreateAsync(new AppUser
                {
                    UserName = model.UserName.ToLower().Trim(),
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email.ToLower().Trim(),
                    ProfileImageUrl = model.ProfileImage.FileName
                }, model.PasswordConfirm);



                if (IdentityResult.Succeeded)
                {
                    var ImgPath = Path.Combine(_webHostEnvironment.WebRootPath, "ProfileImages");

                    if (!Directory.Exists(ImgPath))
                    {
                        Directory.CreateDirectory(ImgPath);
                    }

                    var ImgFullUrl = Path.Combine(ImgPath, model.ProfileImage.FileName);

                    using (var fileFlow = new FileStream(ImgFullUrl, FileMode.Create))
                    {
                        await model.ProfileImage.CopyToAsync(fileFlow);
                    }

                    TempData["success"] = $"merhaba {model.UserName}, aramıza hoşgeldin..!";
                    return RedirectToAction("SignIn", "User");
                }
                else
                {

                    foreach (IdentityError item in IdentityResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                        return View(model);
                    }
                }


            }

            ModelState.AddModelError(string.Empty, "hataları kontrol ediniz");

            return View(model);

        }

        [Route("/gizlilik")]
        public IActionResult Privacy()
        {
            return View();
        }

       
        [Route("/iletisim")]
        [Route("/contact")]
        [HttpGet]
        public IActionResult Contact()
        {
            TempData["success"]="";
            return View();
        }


        [Route("/iletisim")]
        [Route("/contact")]

        [ValidateAntiForgeryToken]     
        [HttpPost]
        public async Task<IActionResult> Contact(ContactViewModel contact)
        
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {

               
                Contact msj = new Contact()
                {
                    Id = new Guid(),
                    Name = contact.Name,
                    Email = contact.Email,
                    Phone = contact.Phone,                   
                    Message = contact.Message,
                    Subject = contact.Subject


                };
              await  _context.Contacts.AddAsync(msj);
              await _context.SaveChangesAsync();

                TempData["success"] = " Mesajınız başarı ile iletildi";

                return View();
                }
                else
                {

                    ModelState.AddModelError(string.Empty, "Mesaj göndermek için üye olmanız gerekmektedir..!");
                    return View(contact);
                }
            }
            else
            {


                ModelState.AddModelError(string.Empty, "bir hatadan dolayı mesajınız iletilemedi");
                return View(contact);
            }
            
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}