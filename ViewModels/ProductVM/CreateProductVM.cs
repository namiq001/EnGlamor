using EnGlamor.Models;

namespace EnGlamor.ViewModels.ProductVM;

public class CreateProductVM
{
    public string ProductName { get; set; } = null!;
    public string ProductDetail { get; set; } = null!;
    public string ProductInformation { get; set; } = null!;
    public string ProductReviews { get; set; } = null!;
    public double ProductPrice { get; set; }
    public int BrandId { get; set; }
    public int CatagoryId { get; set; }
    public List<Brand>? Brands { get; set; }
    public List<Catagory>? Catagories { get; set; }
    public IFormFile Image { get; set; } = null!;


}
