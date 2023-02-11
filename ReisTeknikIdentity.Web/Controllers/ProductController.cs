using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReisTeknikIdentity.Web.Data;
using ReisTeknikIdentity.Web.ViewModels;

namespace ReisTeknikIdentity.Web.Controllers
{
    public class ProductController : Controller
    {

        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Details(string Id)
        {

            var p = await _context.Products.Include(i => i.Images).Include(c=> c.Category).FirstOrDefaultAsync(p => p.Id == Guid.Parse(Id));

            if (p == null) { return NotFound(); }

            ProductDetailViewModel pm = new()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryName = p.Category.Name,
                ImageUrls =  p.Images.Select(x=> x.Name).ToList()

            };
            
            
            return View(pm);
        }
    }
}
