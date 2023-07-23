namespace EnGlamor.Models;

public class Catagory
{
    public int Id { get; set; } 
    public string CatagoryName { get; set; } = null!;
    public List<Product> Products { get; set; }
}
