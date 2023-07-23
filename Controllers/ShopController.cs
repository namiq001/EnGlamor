using EnGlamor.EnGlamorDataBase;
using EnGlamor.Models;
using EnGlamor.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnGlamor.Controllers;

public class ShopController : Controller
{
    private readonly EnGlamorDbContext _context;

    public ShopController(EnGlamorDbContext context)
    {
        _context = context; 
    }

    public async Task<IActionResult> Index(string? search,int? catagoryId,int? brandId)
    {
        IQueryable<Product> query = _context.Products.Include(x=>x.Brands).AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.ProductName.ToLower().Contains(search.ToLower()));
        }
        if(catagoryId != null)
        {
            query = query.Where(p => p.CatagoryId == catagoryId);
        }
        if(brandId != null)
        {
            query = query.Where(p=>p.BrandId == brandId);
        }
        SearchVM index = new SearchVM
        {
            Catagories = await _context.Catagories.ToListAsync(),
            Brands = await _context.Brands.ToListAsync(),
            Products = await query.ToListAsync(),
        };
        return View(index);
    }
}
