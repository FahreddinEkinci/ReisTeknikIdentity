using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ReisTeknikIdentity.Web.Data;
using ReisTeknikIdentity.Web.Models;

namespace ReisTeknikIdentity.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SliderController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        private readonly string imgPath ;

        public SliderController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            imgPath = Path.Combine(_environment.WebRootPath, "SliderImages");
            if (!Directory.Exists(imgPath))
            {

                Directory.Exists(imgPath);

            }

        }

        public async Task<IActionResult> Index()
        {

            return View(await _context.Sliders.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {

            if (ModelState.IsValid)
            {
                var dosyaYolu = Path.Combine(_environment.WebRootPath, "SliderImages");

                if (!Directory.Exists(dosyaYolu))
                {
                    Directory.CreateDirectory(dosyaYolu);
                }
                using (var dosyaAkisi = new FileStream(Path.Combine(dosyaYolu, slider.ImageFile.FileName), FileMode.Create))
                {
                    await slider.ImageFile.CopyToAsync(dosyaAkisi);
                }

                slider.PathImage = slider.ImageFile.FileName;

                await _context.Sliders.AddAsync(slider);
                await _context.SaveChangesAsync();
                TempData["success"] = "kayan resim eklendi";
                return View(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "bir hata meydanı  geldi.");
            return View();
        }

        public IActionResult Edit(Guid Id)
        {

            var slider = _context.Sliders.FindAsync(Id);

            if (slider == null)
            {
                ModelState.AddModelError("", "yanlış alemde geziyorsun koç");
                return View();
            }

            return View(slider);

        }

        [HttpPost]
        public IActionResult Edit(Slider slider)
        {


            return View();
        }


        [HttpPost]
        public IActionResult Delete() { return View(); }



    }
}
