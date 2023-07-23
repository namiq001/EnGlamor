namespace EnGlamor.ViewModels.IconVM;

public class EditIconVM
{
    public string Title { get; set; } = null!;
    public string? IconImageUrl { get; set; }
    public IFormFile? Image { get; set; }
}
