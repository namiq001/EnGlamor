using EnGlamor.EnGlamorDataBase;
using EnGlamor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnGlamor.Controllers;

public class BlogController : Controller
{
    private readonly EnGlamorDbContext _context;

    public BlogController(EnGlamorDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Blog> blogs = await _context.Blogs.ToListAsync();
        return View(blogs);
    }
}
