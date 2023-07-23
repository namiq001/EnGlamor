using System.ComponentModel.DataAnnotations;

namespace EnGlamor.Models;

public class Cantact
{
    public int Id { get; set; }
    [StringLength(maximumLength: 55)]
    public string Name { get; set; } = null!;
    [StringLength(maximumLength: 55)]
    public string Subject { get; set; } = null!;
    [StringLength(maximumLength: 255)]
    public string Message { get; set; } = null!;
    [EmailAddress]
    public string EmailAddress { get; set; } = null!;
    public DateTime? SendedDate { get; set; }
}
