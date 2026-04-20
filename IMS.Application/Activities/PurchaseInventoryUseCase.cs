using System;
using IMS.Application.Activities.Interfaces;
using IMS.Application.Inventories.Interfaces;
using IMS.Domain;

namespace IMS.Application.Activities;

public class PurchaseInventoryUseCase(IInventoryTransactionRepository inventoryTransactionRepository, IInventoryRepository inventoryRepository) : IPurchaseInventoryUseCase
{
    private readonly IInventoryTransactionRepository inventoryTransactionRepository = inventoryTransactionRepository;
    private readonly IInventoryRepository inventoryRepository = inventoryRepository;

    public async Task ExecuteAsync(string poNumber, Inventory inventory, int quantity, string doneBy, double price)
    {
        await inventoryTransactionRepository.PurchaseAsync(poNumber, inventory, quantity, doneBy, price);

        inventory.Quantity += quantity;
        await this.inventoryRepository.UpdateInventoryAsync(inventory);
    }
}