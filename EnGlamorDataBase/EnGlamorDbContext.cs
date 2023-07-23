using EnGlamor.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EnGlamor.EnGlamorDataBase;

public class EnGlamorDbContext : IdentityDbContext<AppUser>
{
    public EnGlamorDbContext(DbContextOptions<EnGlamorDbContext> options) : base(options) 
    { 
    
    }

    public DbSet<Icon> Icons { get; set; }
    public DbSet<Catagory> Catagories { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Setting> Settings { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Cantact> Cantacts { get; set; }
    public DbSet<CartBasketItem> CartBasketItems { get; set; }
    public DbSet<ProductComment> ProductComments { get; set; }
    public DbSet<MailSetting> MailSettings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<MailSetting>()
            .HasKey(m => m.Id);
    }


}
