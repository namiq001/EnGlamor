namespace EnGlamor.ViewModels.BlogVM;

public class EditBlogVM
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? BlogImageUrl { get; set; }
    public IFormFile? Image { get; set; }
}
