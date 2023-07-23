namespace EnGlamor.ViewModels.ProductVM;

public class GetProductVM
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public double ProductPrice { get; set; }
    public string BrandName { get; set; } = null!;
    public string CatagoryName { get; set; } = null!;
    public string? ProductImageUrl { get; set; }

}
