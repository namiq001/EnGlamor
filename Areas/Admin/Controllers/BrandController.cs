using EnGlamor.EnGlamorDataBase;
using EnGlamor.Models;
using EnGlamor.ViewModels.BrandVM;
using EnGlamor.ViewModels.IconVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnGlamor.Areas.Admin.Controllers;
[Area("Admin")]
public class BrandController : Controller
{
    private readonly EnGlamorDbContext _context;

    public BrandController(EnGlamorDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Brand> brands= await _context.Brands.ToListAsync();
        return View(brands);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateBrandVM createBrand)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        Brand? brand = new Brand()
        {
            BrandName = createBrand.BrandName,
        };
        _context.Brands.Add(brand);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
        Brand? brand = await _context
            .Brands
            .AsNoTracking()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        if (brand is null) { return View(); }
        _context.Brands.Remove(brand);
        await   _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(int id)
    {
        Brand? brand = await _context.Brands.FindAsync(id);
        if (brand is null) { return View(); }
        EditBrandVM editBrand = new EditBrandVM()
        {
            BrandName = brand.BrandName,
        };
        return View(editBrand);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EditBrandVM editVM)
    {
        Brand? brand = await _context
            .Brands
            .FirstOrDefaultAsync(x => x.Id == id);
        if (brand is null) { return NotFound(); }
        brand.BrandName = editVM.BrandName;

        _context.Brands.Update(brand);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Detail(int id)
    {
        Brand? brand = await _context.Brands.FindAsync(id);
        if (brand is null)
        {
            return NotFound();
        }

        DetailBrandVM detailBrand = new DetailBrandVM()
        {
            BrandName = brand.BrandName,
        };

        return View(detailBrand);
    }
}
