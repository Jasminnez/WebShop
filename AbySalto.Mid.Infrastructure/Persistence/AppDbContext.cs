using System;
using Microsoft.EntityFrameworkCore;
using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
     public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<UserFavoriteProduct> UserFavoriteProducts { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserFavoriteProduct>()
            .HasKey(ufp => new { ufp.UserId, ufp.ProductId });

        modelBuilder.Entity<UserFavoriteProduct>()
            .HasOne(ufp => ufp.User)
            .WithMany(u => u.FavoriteProducts)
            .HasForeignKey(ufp => ufp.UserId);

        modelBuilder.Entity<UserFavoriteProduct>()
            .HasOne(ufp => ufp.Product)
            .WithMany()
            .HasForeignKey(ufp => ufp.ProductId);
    }
}
