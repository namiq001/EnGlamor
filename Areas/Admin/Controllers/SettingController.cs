using EnGlamor.EnGlamorDataBase;
using EnGlamor.Models;
using EnGlamor.ViewModels.SettingVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnGlamor.Areas.Admin.Controllers;
[Area("Admin")]
public class SettingController : Controller
{
    private readonly EnGlamorDbContext _context;

    public SettingController(EnGlamorDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Setting> settings = await _context.Settings.ToListAsync();
        return View(settings);
    }
    public async Task<IActionResult> Edit(int id)
    {
        Setting? setting = await _context.Settings.FindAsync(id);
        if (setting == null)
        {
            return NotFound();
        }
        EditSettingVM editVM = new EditSettingVM()
        {
            Value = setting.Value,
        };
        return View(editVM);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EditSettingVM newSetting)
    {
        Setting? setting = await _context
            .Settings
            .Where(s => s.Id == id)
            .FirstOrDefaultAsync();
        if (setting == null)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            return View();
        }

        setting.Value = newSetting.Value;
        _context.Settings.Update(setting);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
