using EnGlamor.Models;

namespace EnGlamor.ViewModels;

public class SearchVM
{
    public List<Product> Products { get; set; }
    public List<Catagory> Catagories { get; set; }
    public List<Brand> Brands { get; set; }
}
