using System;
using IMS.Application.Inventories.Interfaces;
using IMS.Domain;

namespace IMS.Application.Inventories;

public class ViewInventoryByIdUseCase(IInventoryRepository inventoryRepository) : IViewInventoryByIdUseCase
{
    private readonly IInventoryRepository inventoryRepository = inventoryRepository;

    public async Task<Inventory> ExecuteAsync(int inventoryID)
    {
        return await inventoryRepository.GetInventoryByIdAsync(inventoryID);
    }
}
