using System;
using IMS.Application.Products.Interfaces;
using IMS.Domain;
using Microsoft.EntityFrameworkCore;

namespace IMS.Persistence.Repositories;

public class ProductEFCoreRepository(IDbContextFactory<AppDbContext> dbContext) : IProductRepository
{
    private readonly IDbContextFactory<AppDbContext> dbContext = dbContext;

    public async Task AddProductAsync(Product product)
    {
        using var db = dbContext.CreateDbContext();
        db.Products?.Add(product);

        FlagInventoryUnchanged(product, db);
        await db.SaveChangesAsync();
    }

    public async Task DeleteProductByIdAsync(int productID)
    {
        using var db = dbContext.CreateDbContext();
        var product = db.Products?.Find(productID);

        if (product is null) return;

        db.Products?.Remove(product);
        await db.SaveChangesAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int productID)
    {
        using var db = dbContext.CreateDbContext();
        return await db.Products?.FirstOrDefaultAsync(p => p.ProductID == productID)!;
    }

    public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
    {
        using var db = dbContext.CreateDbContext();
        return await db.Products?.Where(p => p.ProductName.ToLower().Contains(name.ToLower(), StringComparison.CurrentCulture)).ToListAsync()!;
    }

    public async Task UpdateProductAsync(Product product)
    {
        using var db = dbContext.CreateDbContext();
        var proToUpdate = db.Products?.FirstOrDefault(x => x.ProductID == product.ProductID);

        if (proToUpdate != null)
        {
            proToUpdate.ProductName = product.ProductName;
            proToUpdate.Quantity = product.Quantity;
            proToUpdate.Price = product.Price;
            proToUpdate.ProductInventories = product.ProductInventories;

            FlagInventoryUnchanged(proToUpdate, db);
            await db.SaveChangesAsync();
        }
    }

    private void FlagInventoryUnchanged(Product product, AppDbContext db)
    {
        if (product?.ProductInventories != null &&
            product.ProductInventories.Count > 0)
        {
            foreach (var prodInv in product.ProductInventories)
            {
                if (prodInv.Inventory is not null)
                {
                    db.Entry(prodInv.Inventory).State = EntityState.Unchanged;
                }
            }
        }
    }
}
