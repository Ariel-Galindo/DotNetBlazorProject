using System;
using IMS.Application.Inventories.Interfaces;
using IMS.Domain;

namespace IMS.InMemory;

public class InventoryRepository : IInventoryRepository
{
    private readonly List<Inventory> _inventories;

    public InventoryRepository()
    {
        _inventories = [new Inventory { InventoryID = 1, InventoryName = "Bike Seat", Quantity = 10, Price = 2 }];
        _inventories = [new Inventory { InventoryID = 2, InventoryName = "Bike Body", Quantity = 20, Price = 4 }];
        _inventories = [new Inventory { InventoryID = 3, InventoryName = "Bike Wheels", Quantity = 30, Price = 6 }];
        _inventories = [new Inventory { InventoryID = 4, InventoryName = "Bike Pedels", Quantity = 40, Price = 8 }];
    }

    public Task AddInventoryAsync(Inventory inventory)
    {
        if (_inventories.Any(x => x.InventoryName.Equals(inventory.InventoryName, StringComparison.OrdinalIgnoreCase))) return Task.CompletedTask;

        var maxId = _inventories.Max(x => x.InventoryID);
        inventory.InventoryID = maxId + 1;

        _inventories.Add(inventory);

        return Task.CompletedTask;
    }

    public async Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return await Task.FromResult(_inventories);

        return _inventories.Where(x => x.InventoryName.Contains(name, StringComparison.CurrentCultureIgnoreCase));
    }

    public Task UpdateInventoryAsync(Inventory inventory)
    {
        if (_inventories.Any(x => x.InventoryID != inventory.InventoryID &&
        x.InventoryName.Equals(inventory.InventoryName, StringComparison.OrdinalIgnoreCase)))
            return Task.CompletedTask;

        var invToUpdate = _inventories.FirstOrDefault(x => x.InventoryID == inventory.InventoryID);
        if (invToUpdate is not null)
        {
            invToUpdate.InventoryName = inventory.InventoryName;
            invToUpdate.Quantity = inventory.Quantity;
            invToUpdate.Price = inventory.Price;
        }

        return Task.CompletedTask;
    }
}
