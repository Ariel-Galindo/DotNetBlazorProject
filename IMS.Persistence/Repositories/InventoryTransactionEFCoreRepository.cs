using System;
using IMS.Application.Activities.Interfaces;
using IMS.Domain;
using IMS.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace IMS.Persistence.Repositories;

public class InventoryTransactionEFCoreRepository(IDbContextFactory<AppDbContext> dbContext) : IInventoryTransactionRepository
{
    private readonly IDbContextFactory<AppDbContext> dbContext = dbContext;

    public async Task<IEnumerable<InventoryTransaction>> GetInventoryTransactionAsync(string inventoryName, DateTime? dateFrom, DateTime? dateTo, InventoryTransactionType? transactionType)
    {
        using var db = dbContext.CreateDbContext();

        var query = from it in db.InventoryTransactions
                    join inv in db.Inventories! on it.InventoryID equals inv.InventoryID
                    where
                    (
                        string.IsNullOrWhiteSpace(inventoryName) || inv.InventoryName.ToLower().IndexOf(inventoryName.ToLower()) >= 0)
                        &&
                        (!dateFrom.HasValue || it.TransactionDate >= dateFrom.Value.Date) &&
                        (!dateTo.HasValue || it.TransactionDate <= dateTo.Value.Date) &&
                        (!transactionType.HasValue || it.InventoryTransactionType == transactionType
                    )
                    select it;

        return await query.Include(x => x.Inventory).ToListAsync();
    }

    public async Task ProduceAsync(string productionNumber, Inventory inventory, int quantityToConsume, string doneBy, double price)
    {
        using var db = dbContext.CreateDbContext();

        db.InventoryTransactions?.Add(new InventoryTransaction
        {
            ProductionNumber = productionNumber,
            InventoryID = inventory.InventoryID,
            QuantityBefore = inventory.Quantity,
            QuantityAfter = inventory.Quantity - quantityToConsume,
            InventoryTransactionType = InventoryTransactionType.PurchaseInventory,
            TransactionDate = DateTime.Now,
            DoneBy = doneBy,
            UnitPrice = price
        });

        await db.SaveChangesAsync();
    }

    public async Task PurchaseAsync(string poNumber, Inventory inventory, int quantity, string doneBy, double price)
    {
        using var db = dbContext.CreateDbContext();

        db.InventoryTransactions?.Add(new InventoryTransaction
        {
            PONumber = poNumber,
            InventoryID = inventory.InventoryID,
            QuantityBefore = inventory.Quantity,
            QuantityAfter = inventory.Quantity + quantity,
            InventoryTransactionType = InventoryTransactionType.PurchaseInventory,
            TransactionDate = DateTime.Now,
            DoneBy = doneBy,
            UnitPrice = price
        });

        await db.SaveChangesAsync();
    }
}
