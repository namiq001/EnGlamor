using EnGlamor.Models;

namespace EnGlamor.ViewModels.ProductVM;

public class EditProductVM
{
    public string? ProductName { get; set; } 
    public string? ProductDetail { get; set; } 
    public string? ProductInformation { get; set; } 
    public string? ProductReviews { get; set; } 
    public double ProductPrice { get; set; }
    public int BrandId { get; set; }
    public int CatagoryId { get; set; }
    public string? ProductImageUrl { get; set; }
    public IFormFile? Image { get; set; }
    public List<Catagory>? Catagories { get; set; }
    public List<Brand>? Brands { get; set; }

}
