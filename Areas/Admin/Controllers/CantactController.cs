using EnGlamor.EnGlamorDataBase;
using EnGlamor.Models;
using EnGlamor.ViewModels.IconVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnGlamor.Areas.Admin.Controllers;
[Area("Admin")]
public class CantactController : Controller
{
    private readonly EnGlamorDbContext _context;

    public CantactController(EnGlamorDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Cantact> cantacts = await _context.Cantacts.ToListAsync();
        return View(cantacts);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        Cantact? cantact = await _context
            .Cantacts
            .AsNoTracking()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        if (cantact is null) { return View(); }
        _context.Cantacts.Remove(cantact);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Detail(int id)
    {
        Cantact? cantact = await _context.Cantacts.FindAsync(id);
        if (cantact is null)
        {
            return NotFound();
        }
        return View(cantact);
    }
}
