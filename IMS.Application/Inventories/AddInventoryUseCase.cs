using System;
using IMS.Application.Inventories.Interfaces;
using IMS.Domain;

namespace IMS.Application.Inventories;

public class AddInventoryUseCase(IInventoryRepository inventoryRepository) : IAddInventoryUseCase
{
    private readonly IInventoryRepository inventoryRepository = inventoryRepository;

    public async Task ExecuteAsync(Inventory inventory)
    {
        await inventoryRepository.AddInventoryAsync(inventory);
    }
}