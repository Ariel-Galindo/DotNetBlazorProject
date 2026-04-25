using System;
using IMS.Application.Activities.Interfaces;
using IMS.Application.Inventories.Interfaces;
using IMS.Application.Products.Interfaces;
using IMS.Domain;
using IMS.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace IMS.Persistence.Repositories;

public class ProductTransactionEFCoreRepository(
    IInventoryTransactionRepository iventoryTransactionRepository,
    IInventoryRepository inventoryRepository,
    IDbContextFactory<AppDbContext> dbContext) : IProductTransactionRepository
{
    private readonly IInventoryTransactionRepository iventoryTransactionRepository = iventoryTransactionRepository;
    private readonly IInventoryRepository inventoryRepository = inventoryRepository;
    private readonly IDbContextFactory<AppDbContext> dbContext = dbContext;

    public async Task ProduceAsync(string productionNumber, Product product, int quantityToConsume, string doneBy)
    {
        using var db = dbContext.CreateDbContext();

        var prod = await db.Products!.FindAsync(product.ProductID);

        if (prod != null)
        {
            foreach (var item in prod.ProductInventories)
            {
                if (item.Inventory != null)
                {
                    await this.iventoryTransactionRepository.ProduceAsync(
                        productionNumber,
                        item.Inventory,
                        item.Inventory!.Quantity * quantityToConsume,
                        doneBy,
                         -1);

                    var inv = await this.inventoryRepository.GetInventoryByIdAsync(item.InventoryID);
                    inv!.Quantity -= item.InventoryQuantity * quantityToConsume;
                    await this.inventoryRepository.UpdateInventoryAsync(inv);
                }
            }
        }

        db.ProductTransactions?.Add(new ProductTransaction
        {
            ProductionNumber = productionNumber,
            ProductID = product.ProductID,
            QuantityBefore = product.Quantity,
            QuantityAfter = product.Quantity + quantityToConsume,
            ProductTransactionType = ProductTransactionType.ProduceProduct,
            TransactionDate = DateTime.Now,
            DoneBy = doneBy
        });

        await db.SaveChangesAsync();
    }

    public async Task SellProductAsync(string salesOrderNumber, Product product, int quantity, double unitPrice, string doneBy)
    {
        using var db = dbContext.CreateDbContext();

        db.ProductTransactions?.Add(new ProductTransaction
        {
            ProductTransactionType = ProductTransactionType.SellProduct,
            SONumber = salesOrderNumber,
            ProductID = product.ProductID,
            QuantityBefore = product.Quantity,
            QuantityAfter = product.Quantity - quantity,
            TransactionDate = DateTime.Now,
            UnitPrice = product.Price
        });

        await db.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProductTransaction>> GetProductTransactionAsync(string productName, DateTime? dateFrom, DateTime? dateTo, ProductTransactionType? transactionType)
    {
        using var db = dbContext.CreateDbContext();

        var query = from it in db.ProductTransactions
                    join pro in db.Products! on it.ProductID equals pro.ProductID
                    where
                    (
                        string.IsNullOrWhiteSpace(productName) || pro.ProductName.ToLower().IndexOf(productName.ToLower()) >= 0)
                        &&
                        (!dateFrom.HasValue || it.TransactionDate >= dateFrom.Value.Date) &&
                        (!dateTo.HasValue || it.TransactionDate <= dateTo.Value.Date) &&
                        (!transactionType.HasValue || it.ProductTransactionType == transactionType
                    )
                    select it;

        return await query.Include(x => x.Product).ToListAsync();
    }
}

