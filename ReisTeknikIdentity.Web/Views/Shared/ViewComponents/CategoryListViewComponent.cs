using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReisTeknikIdentity.Web.Data;
using ReisTeknikIdentity.Web.ViewModels;

namespace ReisTeknikIdentity.Web.Views.Shared.ViewComponents
{
    public class CategoryListViewComponent:ViewComponent
    {
        public readonly AppDbContext _context;

        public CategoryListViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.Categories
                .Select(x => new CategoryListComponentViewModel() { Name = x.Name, Id = x.Id.ToString() })
                .ToListAsync();
              

            return View(categories);
        }

    }
}
