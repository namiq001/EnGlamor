using EnGlamor.EnGlamorDataBase;
using EnGlamor.Models;
using EnGlamor.ViewModels.BlogVM;
using EnGlamor.ViewModels.IconVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnGlamor.Areas.Admin.Controllers;
[Area("Admin")]
public class BlogController : Controller
{
    private readonly EnGlamorDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public BlogController(IWebHostEnvironment environment, EnGlamorDbContext context)
    {
        _context = context; 
        _environment = environment;
    }

    public async Task<IActionResult> Index()
    {
        List<Blog> icons = await _context.Blogs.ToListAsync();
        return View(icons);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateBlogVM createBlog)
    {
        if (!ModelState.IsValid)
        {
            return View(createBlog);
        }
        string newFileName = Guid.NewGuid().ToString() + createBlog.Image.FileName;
        string path = Path.Combine(_environment.WebRootPath, "assets", "img", "blog", newFileName);
        using (FileStream stream = new FileStream(path, FileMode.CreateNew))
        { 
            await createBlog.Image.CopyToAsync(stream);
        }
        Blog blog = new Blog()
        {
            Title = createBlog.Title,
            Description = createBlog.Description,
        };
        blog.BlogImageUrl = newFileName;

        _context.Blogs.Add(blog);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int Id)
    {
        Blog? blog = await _context.Blogs.FindAsync(Id);
        if (blog is null)
        {
            return NotFound();
        }
        string path = Path.Combine(_environment.WebRootPath, "assets", "img", "Blog", blog.BlogImageUrl);
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
        _context.Blogs.Remove(blog);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int Id)
    {
        Blog? blog = await _context.Blogs.FindAsync(Id);
        if (blog is null)
        {
            return NotFound();
        }
        EditBlogVM editBlog = new EditBlogVM()
        {
            Title = blog.Title,
            Description = blog.Description,
            BlogImageUrl = blog.BlogImageUrl,
        };
        return View(editBlog);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int Id, EditBlogVM editBlog)
    {
        Blog? blog = await _context.Blogs.FindAsync(Id);
        if (blog is null)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            return View(editBlog);
        }
        if (editBlog.Image is not null)
        {
            string path = Path.Combine(_environment.WebRootPath, "assets", "img", "Blog", blog.BlogImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            string newFileName = Guid.NewGuid().ToString() + editBlog.Image.FileName;
            string newPath = Path.Combine(_environment.WebRootPath, "assets", "img", "Blog" +
                "", newFileName);
            using (FileStream stream = new FileStream(newPath, FileMode.CreateNew))
            {
                await editBlog.Image.CopyToAsync(stream);
            }
            blog.BlogImageUrl = newFileName;
        }
        blog.Title = editBlog.Title;
        blog.Description = editBlog.Description;
        _context.Blogs.Update(blog);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Detail(int id)
    {
        Blog? blog = await _context.Blogs.FindAsync(id);
        if (blog is null)
        {
            return NotFound();
        }

        DetailBlogVM detailBlog = new DetailBlogVM()
        {
            Title = blog.Title,
            Description = blog.Description,
            BlogImageUrl = blog.BlogImageUrl,
        };

        return View(detailBlog);
    }
}
