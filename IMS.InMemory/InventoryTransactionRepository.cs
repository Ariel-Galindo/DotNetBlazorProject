using IMS.Application.Activities.Interfaces;
using IMS.Domain;
using IMS.Domain.Enums;

namespace IMS.InMemory;

public class InventoryTransactionRepository : IInventoryTransactionRepository
{
    public List<InventoryTransaction> _inventoryTransaction = new List<InventoryTransaction>();

    public void ProduceAsync(string productionNumber, Inventory inventory, int quantityToConsume, string doneBy, double price)
    {
        this._inventoryTransaction.Add(new InventoryTransaction
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
    }

    public void PurchaseAsync(string poNumber, Inventory inventory, int quantity, string doneBy, double price)
    {
        this._inventoryTransaction.Add(new InventoryTransaction
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
    }
}