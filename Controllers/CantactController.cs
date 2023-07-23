using EnGlamor.EnGlamorDataBase;
using EnGlamor.Models;
using EnGlamor.ViewModels.CantactVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnGlamor.Controllers;

public class CantactController : Controller
{
    private readonly EnGlamorDbContext _context;

    public CantactController(EnGlamorDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Index(SendVM send)
    {
        if (!ModelState.IsValid)
        {
            return View(send);
        }

        Cantact cantact = new Cantact
        {
            Name = send.Name,
            Subject = send.Subject,
            EmailAddress = send.EmailAddress,
            Message = send.Message,
            SendedDate = DateTime.UtcNow.AddHours(4),
        };

        _context.Cantacts.Add(cantact);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
 