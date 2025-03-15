using Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Persistence
{

    public class FoodOrderDbContext : IdentityDbContext<AppUser, IdentityRole<long>, long>, IUnitOfWork
    {
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public FoodOrderDbContext(DbContextOptions<FoodOrderDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var menuItem = modelBuilder.Entity<MenuItem>();
            menuItem.HasKey(m => m.Id);
            menuItem.Property(m => m.Id).ValueGeneratedOnAdd();

            var order = modelBuilder.Entity<Order>();
            order.HasKey(o => o.Id);
            order.Property(o => o.Id).ValueGeneratedOnAdd();
            order.HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.CustomerId);
            order.HasOne<AppUser>()
                .WithMany()
                .HasForeignKey(o => o.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull);

            var orderItem = modelBuilder.Entity<OrderItem>();
            orderItem.HasKey(oi => oi.Id);
            orderItem.Property(oi => oi.Id).ValueGeneratedOnAdd();
            orderItem.HasOne<Order>()
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            orderItem.HasOne<MenuItem>()
                .WithMany()
                .HasForeignKey(oi => oi.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            var customer = modelBuilder.Entity<Customer>();
            customer.HasKey(c => c.Id);
            customer.Property(c => c.Id).ValueGeneratedOnAdd();
        }

        public async Task SaveChangesAsync() => await base.SaveChangesAsync();
    }
}