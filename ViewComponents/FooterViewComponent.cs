﻿using EnGlamor.EnGlamorDataBase;
using EnGlamor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnGlamor.ViewComponents;

public class FooterViewComponent : ViewComponent
{
    private readonly EnGlamorDbContext _context;

    public FooterViewComponent(EnGlamorDbContext context)
    {
        _context = context;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        Dictionary<string, Setting> setting = await _context.Settings.ToDictionaryAsync(x => x.Key);
        return View(setting);
    }
}
