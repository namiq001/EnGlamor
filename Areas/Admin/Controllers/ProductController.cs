using EnGlamor.EnGlamorDataBase;
using EnGlamor.Models;
using EnGlamor.ViewModels.ProductVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnGlamor.Areas.Admin.Controllers;
[Area("Admin")]
public class ProductController : Controller
{
    private readonly EnGlamorDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public ProductController(IWebHostEnvironment environment,EnGlamorDbContext context)
    {
        _context = context;
        _environment = environment;
    }

    public async Task<IActionResult> Index()
    {
        List<Product> products = await _context
            .Products
            .Include(x=>x.Brands)
            .Include(x=>x.Catagories)
            .ToListAsync();

        List<GetProductVM> getProductList = products.Select(p => new GetProductVM
        {
            ProductId=p.Id,
            ProductName = p.ProductName,
            ProductPrice = p.ProductPrice,
            BrandName = p.Brands.BrandName,
            CatagoryName = p.Catagories.CatagoryName,
            ProductImageUrl = p.ProductImageUrl
        }).ToList();

        return View(getProductList);
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        CreateProductVM createProduct = new CreateProductVM()
        {
            Catagories = await _context.Catagories.ToListAsync(),
            Brands = await _context.Brands.ToListAsync(),
        };
        return View(createProduct);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateProductVM createProduct)
    {
        createProduct.Catagories = await _context.Catagories.ToListAsync();
        createProduct.Brands = await _context.Brands.ToListAsync();
        if (!ModelState.IsValid)
        {
            return View(createProduct);
        }
        string newFileName = Guid.NewGuid().ToString() + createProduct.Image.FileName;
        string path = Path.Combine(_environment.WebRootPath, "assets", "img", "products", newFileName);
        using (FileStream stream = new FileStream(path, FileMode.CreateNew))
        {
            await createProduct.Image.CopyToAsync(stream);
        }

        Product product = new Product()
        {
            ProductName = createProduct.ProductName,
            ProductDetail = createProduct.ProductDetail,
            ProductInformation = createProduct.ProductInformation,
            ProductReviews = createProduct.ProductReviews,
            ProductPrice = createProduct.ProductPrice,
            BrandId = createProduct.BrandId,
            CatagoryId = createProduct.CatagoryId,
        };
        product.ProductImageUrl = newFileName;

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int Id)
    {
        Product? product = await _context.Products.FindAsync(Id);
        if (product is null) { return NotFound(); }
        string path = Path.Combine(_environment.WebRootPath, "assets", "img", "products", product.ProductImageUrl);
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int Id)
    {
        Product? product = await _context.Products.FindAsync(Id);
        if (product is null)
        {
            return NotFound();
        }
        EditProductVM editProduct = new EditProductVM()
        {
            ProductName = product.ProductName,
            ProductDetail = product.ProductDetail,
            ProductInformation = product.ProductInformation,
            ProductReviews = product.ProductReviews,
            ProductPrice = product.ProductPrice,
            BrandId = product.BrandId,
            CatagoryId = product.CatagoryId,
            Catagories = await _context.Catagories.ToListAsync(),
            Brands = await _context.Brands.ToListAsync(),
            ProductImageUrl = product.ProductImageUrl
        };
        return View(editProduct);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int Id, EditProductVM editProduct)
    {
        Product? product = await _context.Products.FindAsync(Id);
        if (product is null)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            editProduct.Catagories = await _context.Catagories.ToListAsync();
            return View(editProduct);
        }
        if (editProduct.Image is not null)
        {
            string path = Path.Combine(_environment.WebRootPath, "assets", "img", "products", product.ProductImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            string newFileName = Guid.NewGuid().ToString() + editProduct.Image.FileName;
            string newPath = Path.Combine(_environment.WebRootPath, "assets", "img", "products", newFileName);
            using (FileStream stream = new FileStream(newPath, FileMode.CreateNew))
            {
                await editProduct.Image.CopyToAsync(stream);
            }
            product.ProductImageUrl = newFileName;
        }
        product.ProductName = editProduct.ProductName;
        product.ProductDetail = editProduct.ProductDetail;
        product.ProductInformation = editProduct.ProductInformation;
        product.ProductReviews = editProduct.ProductReviews;
        product.ProductPrice = editProduct.ProductPrice;
        product.BrandId = editProduct.BrandId;
        product.CatagoryId = editProduct.CatagoryId;
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Detail(int id)
    {
        Product? product = await _context.Products.Include(x => x.Catagories).Include(x=>x.Brands).FirstOrDefaultAsync(x => x.Id == id);
        if (product is null)
        {
            return NotFound();
        }
        return View(product);
    }
}
