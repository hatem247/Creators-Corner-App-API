using Creators_Corner_App_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Creators_Corner_App_API.Data
{
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<BrandApplication> BrandApplications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Unique constraints
            modelBuilder.Entity<Customer>()
            .HasIndex(c => c.Username)
            .IsUnique();

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Brand>()
                .HasIndex(b => b.Username)
                .IsUnique();

            modelBuilder.Entity<Brand>()
                .HasIndex(c => c.Email)
                .IsUnique();

            // Relationships
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(b => b._Products)
                .HasForeignKey(p => p.BrandId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c._Orders)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Customer)
                .WithMany()
                .HasForeignKey(c => c.CustomerId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o._Products)
                .WithMany(p => p._Orders)
                .UsingEntity(j => j.ToTable("OrderProducts"));

            modelBuilder.Entity<Cart>()
                .HasMany(c => c._Products)
                .WithMany(p => p._Carts)
                .UsingEntity(j => j.ToTable("CartProducts"));
        }
    }
}