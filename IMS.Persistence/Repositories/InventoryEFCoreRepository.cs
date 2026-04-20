using System;
using IMS.Application.Inventories.Interfaces;
using IMS.Domain;
using Microsoft.EntityFrameworkCore;

namespace IMS.Persistence.Repositories;

public class InventoryEFCoreRepository(IDbContextFactory<AppDbContext> dbContext) : IInventoryRepository
{
    private readonly IDbContextFactory<AppDbContext> dbContext = dbContext;

    public async Task AddInventoryAsync(Inventory inventory)
    {
        using var db = dbContext.CreateDbContext();
        db.Inventories?.Add(inventory);
        await db.SaveChangesAsync();
    }

    public async Task DeleteInventoryByIdAsync(int inventoryID)
    {
        using var db = dbContext.CreateDbContext();
        var inventory = db.Inventories?.Find(inventoryID);

        if (inventory is null) return;

        db.Inventories?.Remove(inventory);
        await db.SaveChangesAsync();
    }

    public async Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name)
    {
        using var db = dbContext.CreateDbContext();
        return await db.Inventories?.Where(i => i.InventoryName.ToLower().Contains(name.ToLower(), StringComparison.CurrentCulture)).ToListAsync()!;
    }

    public async Task<Inventory?> GetInventoryByIdAsync(int inventoryID)
    {
        using var db = dbContext.CreateDbContext();
        return await db.Inventories?.FirstOrDefaultAsync(i => i.InventoryID == inventoryID)!;
    }

    public async Task UpdateInventoryAsync(Inventory inventory)
    {
        using var db = dbContext.CreateDbContext();
        var invToUpdate = db.Inventories?.FirstOrDefault(x => x.InventoryID == inventory.InventoryID);

        if (invToUpdate is not null)
        {
            invToUpdate.InventoryName = inventory.InventoryName;
            invToUpdate.Quantity = inventory.Quantity;
            invToUpdate.Price = inventory.Price;
            await db.SaveChangesAsync();
        }
    }
}
