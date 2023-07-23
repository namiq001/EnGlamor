using EnGlamor.EnGlamorDataBase;
using EnGlamor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnGlamor.Controllers;

public class AboutController : Controller
{
    private readonly EnGlamorDbContext _context;

    public AboutController(EnGlamorDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Icon> icons = await _context.Icons.ToListAsync();
        return View(icons);
    }
}
