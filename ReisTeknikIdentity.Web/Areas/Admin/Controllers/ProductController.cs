using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReisTeknikIdentity.Web.Areas.Admin.ViewModels;
using ReisTeknikIdentity.Web.Data;
using ReisTeknikIdentity.Web.Models;

namespace ReisTeknikIdentity.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private string dosyaYolu;

        public ProductController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            dosyaYolu = Path.Combine(_hostEnvironment.WebRootPath, "ProductImages");

            if (!Directory.Exists(dosyaYolu))
            {
                Directory.CreateDirectory(dosyaYolu);  //ilgili klasör yoksa oluştur
            }
        }



        // GET: Admin/Product
        public async Task<IActionResult> Index()
        {
            //return await _context.Products.ToListAsync() != null ?
            //            View(await _context.Products
            //            .Include(x => x.Images.Where(r => r.Product.Id == x.Id))
            //            .Include(x => x.Category)
            //            .ToListAsync()) :
            //            Problem("Entity set 'AppDbContext.Products'  is null.");


            var produts = _context.Products
                .Include(img => img.Images);


            return View(await produts.ToListAsync());


        }

        // GET: Admin/Product/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(x => x.Category).Include(x => x.Images)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Product/Create
        public IActionResult Create()
        {
            var categories = _context.Categories.ToList();

            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Title,Brand,Price,Stock,State,Id,CreatedDate,UpdatedDate, Files, CategoryId, CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {


                product.Id = Guid.NewGuid();

                if (product.Files != null)
                {


                    foreach (var item in product.Files)
                    {
                        using (var dosyaAkisi = new FileStream(Path.Combine(dosyaYolu, item.FileName), FileMode.Create))
                        {
                            await item.CopyToAsync(dosyaAkisi);
                            product.Images.Add(new Image() { Id = new Guid(), Name = item.FileName });


                        }
                    }


                }


                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var categories = _context.Categories.ToList();

            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(product);
        }

        // GET: Admin/Product/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {


            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var categories = await _context.Categories.ToListAsync();
            ViewBag.Categories = categories;

            var product = await _context.Products.Include(x=> x.Category).Include(i => i.Images).SingleOrDefaultAsync(x=> x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Description,Title,Brand,Price,Stock,State,Id,CreatedDate,UpdatedDate, Files")] Product product)
        {

            var categories = await _context.Categories.ToListAsync();
            TempData["Categories"] = categories;

            product.UpdatedDate = DateTime.UtcNow;
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Admin/Product/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'AppDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveProdutsImage(string id)
        {
            var img = await _context.Images.FindAsync(id); //id'si verilen img bulundu
            _context.Images.Remove(img); //bulduğumuz img varlığını Resimler listesinden kaldırdık
            await _context.SaveChangesAsync(); //Bu değişiklikleri veritabanına yansıttık

            System.IO.File.Delete(Path.Combine(dosyaYolu, img.Name)); // dosyayı sildik.

            TempData["successMessage"] = "resim silindi";

            return RedirectToAction(nameof(Edit), new { id = img.Product.Id });
        }

        [HttpPost]
        public async Task<IActionResult> AddImageToProduct(Product product)
        {
            var ProductToBePhotographed = await _context.Products.FindAsync(product.Id);
            try
            {
                foreach (var item in product.Files)
                {
                    var tamDosyaAdi = Path.Combine(dosyaYolu, item.FileName);

                    using (var dosyaAkisi = new FileStream(tamDosyaAdi, FileMode.Create))
                    {
                        await item.CopyToAsync(dosyaAkisi); //server'a upload
                    }

                    ProductToBePhotographed.Images.Add(new () { Name = item.FileName });
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Edit), new { id = product.Id });
            }
            catch (Exception e)
            {

                TempData["success"] = "Lütfen önce yüklenecek dosyaları seçiniz" + e.Message;
                return RedirectToAction(nameof(Edit), new { id = product.Id });
            }


        }

        private bool ProductExists(Guid id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
