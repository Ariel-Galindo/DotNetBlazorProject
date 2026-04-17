using System;
using IMS.Domain;
using IMS.Domain.Enums;

namespace IMS.Application.Activities.Interfaces;

public interface IInventoryTransactionRepository
{
    void PurchaseAsync(string poNumber, Inventory inventory, int quantity, string doneBy, double price);
    void ProduceAsync(string productionNumber, Inventory inventory, int quantityToConsume, string doneBy, double price);
    Task<IEnumerable<InventoryTransaction>> GetInventoryTransactionAsync(string inventoryName, DateTime? dateFrom, DateTime? dateTo, InventoryTransactionType? transactionType);
}
