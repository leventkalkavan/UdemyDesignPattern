using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Composite.Models;

namespace WebApp.Composite.Context;

public class AppDbContext: IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Book> Books { get; set; }

}
 