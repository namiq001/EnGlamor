namespace EnGlamor.Models;

public class Blog
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string BlogImageUrl { get; set; } = null!;
}
