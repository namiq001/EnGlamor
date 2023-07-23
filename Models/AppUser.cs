using Microsoft.AspNetCore.Identity;

namespace EnGlamor.Models;

public class AppUser : IdentityUser
{
    public string Name { get; set; } = null!;
}
