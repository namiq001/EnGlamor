using System.ComponentModel.DataAnnotations;

namespace EnGlamor.ViewModels.CantactVM;

public class SendVM
{
    [EmailAddress]
    public string EmailAddress { get; set; } = null!;
    public string Message { get; set; }
    public string Name { get; set; }
    public string Subject { get; set; }
}
