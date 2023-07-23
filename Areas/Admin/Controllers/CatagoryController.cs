using EnGlamor.EnGlamorDataBase;
using EnGlamor.Models;
using EnGlamor.ViewModels.BrandVM;
using EnGlamor.ViewModels.CatagoryVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnGlamor.Areas.Admin.Controllers;
[Area("Admin")]
public class CatagoryController : Controller
{
    private readonly EnGlamorDbContext _context;

    public CatagoryController(EnGlamorDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Catagory> catagories = await _context.Catagories.ToListAsync();
        return View(catagories);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateCatagoryVM createCatagory)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        Catagory? catagory = new Catagory()
        {
            CatagoryName = createCatagory.CatagoryName,
        };
        _context.Catagories.Add(catagory);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        Catagory? catagory = await _context
            .Catagories
            .AsNoTracking()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        if (catagory is null) { return View(); }
        _context.Catagories.Remove(catagory);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(int id)
    {
        Catagory? catagory = await _context.Catagories.FindAsync(id);
        if (catagory is null) { return View(); }
        EditCatagoryVM editCatagory = new EditCatagoryVM()
        {
            CatagoryName = catagory.CatagoryName,
        };
        return View(editCatagory);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EditCatagoryVM editVM)
    {
        Catagory? catagory = await _context
            .Catagories
            .FirstOrDefaultAsync(x => x.Id == id);
        if (catagory is null) { return NotFound(); }
        catagory.CatagoryName = editVM.CatagoryName;

        _context.Catagories.Update(catagory);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Detail(int id)
    {
        Catagory? catagory = await _context.Catagories.FindAsync(id);
        if (catagory is null)
        {
            return NotFound();
        }

        DetailCatagoryVM detailCatagory = new DetailCatagoryVM()
        {
            CatagoryName = catagory.CatagoryName,
        };

        return View(detailCatagory);
    }

}
