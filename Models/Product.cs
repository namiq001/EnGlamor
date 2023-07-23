namespace EnGlamor.Models;

public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; } = null!;
    public string ProductDetail { get; set; } = null!;
    public string ProductInformation { get; set; } = null!;
    public string ProductReviews { get; set; } = null!;
    public double ProductPrice { get; set; }
    public string ProductImageUrl { get; set; } = null!;
    public int BrandId { get; set; }
    public Brand Brands { get; set; }
    public int CatagoryId { get; set; }
    public Catagory Catagories { get; set; }
    public List<ProductComment>? ProductComments { get; set; }

}
