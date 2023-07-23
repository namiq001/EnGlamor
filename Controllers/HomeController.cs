using EnGlamor.EnGlamorDataBase;
using EnGlamor.Models;
using EnGlamor.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnGlamor.Controllers;
 
public class HomeController : Controller
{
    private readonly EnGlamorDbContext _context;

    public HomeController(EnGlamorDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Icon> icons = await _context.Icons.ToListAsync();
        List<Product> products = await _context.Products.Include(x=>x.Brands).ToListAsync();
        HomeVM homeVM = new HomeVM()
        {
            Products = products,
            Icons = icons,
        };
        return View(homeVM);
    }
}