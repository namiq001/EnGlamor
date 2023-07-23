using EnGlamor.EnGlamorDataBase;
using EnGlamor.Models;
using EnGlamor.ViewModels.IconVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnGlamor.Areas.Admin.Controllers;
[Area("Admin")]
public class IconController : Controller
{
    private readonly EnGlamorDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public IconController(EnGlamorDbContext context, IWebHostEnvironment environment) 
    {
        _context = context;
        _environment = environment;
    }

    public async Task<IActionResult> Index()    
    {
        List<Icon> icons = await _context.Icons.ToListAsync();
        return View(icons);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateIconVM createIcon)
    {
        if(!ModelState.IsValid)
        {
            return View(createIcon);
        }
        string newFileName = Guid.NewGuid().ToString() + createIcon.Image.FileName;
        string path = Path.Combine(_environment.WebRootPath, "assets", "img", "features", newFileName);
        using (FileStream stream = new FileStream(path, FileMode.CreateNew))
        {
            await createIcon.Image.CopyToAsync(stream);
        }
        Icon icon = new Icon()
        {
            Title = createIcon.Title,
        };
        icon.IconImageUrl = newFileName;

        _context.Icons.Add(icon);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int Id)
    {
        Icon? icon = await _context.Icons.FindAsync(Id);
        if (icon is null)
        {
            return NotFound();
        }
        string path = Path.Combine(_environment.WebRootPath, "assets", "img", "features", icon.IconImageUrl);
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
        _context.Icons.Remove(icon);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int Id)
    {
        Icon? icon = await _context.Icons.FindAsync(Id);
        if (icon is null)
        {
            return NotFound();
        }
        EditIconVM editIcon = new EditIconVM()
        {
            Title = icon.Title,
            IconImageUrl = icon.IconImageUrl,
        };
        return View(editIcon);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int Id, EditIconVM editIcon)
    {
        Icon? icon = await _context.Icons.FindAsync(Id);
        if (icon is null)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            return View(editIcon);
        }
        if (editIcon.Image is not null)
        {
            string path = Path.Combine(_environment.WebRootPath, "assets", "img", "features", icon.IconImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            string newFileName = Guid.NewGuid().ToString() + editIcon.Image.FileName;
            string newPath = Path.Combine(_environment.WebRootPath, "assets", "img", "features" +
                "", newFileName);
            using (FileStream stream = new FileStream(newPath, FileMode.CreateNew))
            {
                await editIcon.Image.CopyToAsync(stream);
            }
            icon.IconImageUrl = newFileName;
        }
        icon.Title = editIcon.Title;
        _context.Icons.Update(icon);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Detail(int id)
    {
        Icon? icon = await _context.Icons.FindAsync(id);
        if (icon is null)
        {
            return NotFound();
        }

        DetailIconVM detailIcon = new DetailIconVM()
        {
            Title = icon.Title,
            IconImageUrl = icon.IconImageUrl,
        };

        return View(detailIcon);
    }
}
