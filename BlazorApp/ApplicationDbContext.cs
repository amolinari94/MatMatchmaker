using Microsoft.EntityFrameworkCore;
using BlazorApp.Components;
namespace Structure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Define DbSet properties for your entities
    public DbSet<UserProfile> UserProfiles { get; set; }
}
