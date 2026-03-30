using System;
using IMS.Application.Interfaces;
using IMS.Domain;

namespace IMS.Application.Inventories;

public class ViewInventoriesByNameUseCase
{
    private readonly IInventoryRepository inventoryRepository;

    public ViewInventoriesByNameUseCase(IInventoryRepository inventoryRepository)
    {
        this.inventoryRepository = inventoryRepository;
    }

    public async Task<IEnumerable<Inventory>> ExecuteAsync(string name)
    {
        return await inventoryRepository.GetInventoriesByNameAsync(name);
    }
}
