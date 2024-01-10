using Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Model.Data;

public class BertLunchContext : IdentityDbContext<AppUser>
{
    public BertLunchContext(DbContextOptions<BertLunchContext> options)
        : base(options)
    {
    }

    public DbSet<MenuItem> MenuItem { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<MenuCategory> MenuCategories { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Setting the configuration: if Category or MenuCategory is deleted, the MenuItems having these fields will be set to null
        builder.Entity<MenuItem>()
            .HasOne(mi => mi.Category)
            .WithMany(c => c.MenuItems)
            .HasForeignKey(mi => mi.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<MenuItem>()
            .HasOne(mi => mi.MenuCategory)
            .WithMany(c => c.MenuItems)
            .HasForeignKey(mi => mi.MenuCategoryId)
            .OnDelete(DeleteBehavior.SetNull);
        
        // Setting the Review to be null when user delete's the account
        builder.Entity<Review>()
            .HasOne(u => u.User)
            .WithMany(x => x.UserReviews)
            .HasForeignKey(x => x.AppUserId)
            .OnDelete(DeleteBehavior.SetNull);
    }


}
