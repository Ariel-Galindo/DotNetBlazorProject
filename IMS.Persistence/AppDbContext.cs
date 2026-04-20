using System;
using IMS.Domain;
using Microsoft.EntityFrameworkCore;

namespace IMS.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Inventory>? Inventories { get; set; }
    public DbSet<Product>? Products { get; set; }
    public DbSet<ProductInventory>? ProductInventories { get; set; }
    public DbSet<InventoryTransaction>? InventoryTransactions { get; set; }
    public DbSet<ProductTransaction>? ProductTransactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductInventory>()
            .HasKey(pi => new { pi.ProductID, pi.InventoryID });

        modelBuilder.Entity<ProductInventory>()
            .HasOne(pi => pi.Product)
            .WithMany(p => p.ProductInventories)
            .HasForeignKey(pi => pi.ProductID);

        modelBuilder.Entity<ProductInventory>()
            .HasOne(pi => pi.Inventory)
            .WithMany(i => i.ProductInventories)
            .HasForeignKey(pi => pi.InventoryID);

        modelBuilder.Entity<Inventory>()
            .HasData([
            new Inventory { InventoryID = 1, InventoryName = "Bike Seat", Quantity = 10, Price = 2 },
            new Inventory { InventoryID = 2, InventoryName = "Bike Body", Quantity = 20, Price = 4 },
            new Inventory { InventoryID = 3, InventoryName = "Bike Wheel", Quantity = 30, Price = 6 },
            new Inventory { InventoryID = 4, InventoryName = "Bike Pedal", Quantity = 40, Price = 8 }
        ]);

        modelBuilder.Entity<Product>()
            .HasData([
            new Product { ProductID = 1, ProductName = "Bike", Quantity = 10, Price = 200 },
            new Product { ProductID = 2, ProductName = "Car", Quantity = 20, Price = 400 },
        ]);

        modelBuilder.Entity<ProductInventory>()
            .HasData([
            new ProductInventory { ProductID = 1, InventoryID = 1, InventoryQuantity = 10 },
            new ProductInventory { ProductID = 1, InventoryID = 2, InventoryQuantity = 20 },
            new ProductInventory { ProductID = 1, InventoryID = 3, InventoryQuantity = 20 },
            new ProductInventory { ProductID = 1, InventoryID = 4, InventoryQuantity = 20 },
        ]);
    }
}
