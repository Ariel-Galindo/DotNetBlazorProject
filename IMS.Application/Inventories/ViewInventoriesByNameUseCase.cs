using System;
using IMS.Application.Inventories.Interfaces;
using IMS.Domain;

namespace IMS.Application.Inventories;

public class ViewInventoriesByNameUseCase(IInventoryRepository inventoryRepository) : IViewInventoriesByNameUseCase
{
    private readonly IInventoryRepository inventoryRepository = inventoryRepository;

    public async Task<IEnumerable<Inventory>> ExecuteAsync(string name = "")
    {
        return await inventoryRepository.GetInventoriesByNameAsync(name);
    }
}
