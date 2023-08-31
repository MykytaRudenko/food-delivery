using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Data;

public class ApplicationContext: DbContext
{
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Dish> Dishes => Set<Dish>();
    public DbSet<Establishment> Establishments => Set<Establishment>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderedDish> OrderedDishes => Set<OrderedDish>();
    public DbSet<User> Users => Set<User>();
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options) 
        : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderedDish>()
            .HasOne(od => od.Dish)
            .WithMany(d => d.OrderedDishes)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<OrderedDish>()
            .HasOne(od => od.Order)
            .WithMany(o => o.OrderedDishes)
            .OnDelete(DeleteBehavior.Restrict);
    }
}