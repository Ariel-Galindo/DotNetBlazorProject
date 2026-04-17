using IMS.Application.Activities.Interfaces;
using IMS.Application.Inventories.Interfaces;
using IMS.Domain;
using IMS.Domain.Enums;

namespace IMS.InMemory;

public class InventoryTransactionRepository(IInventoryRepository inventoryRepository) : IInventoryTransactionRepository
{
    private readonly IInventoryRepository inventoryRepository = inventoryRepository;
    public List<InventoryTransaction> _inventoryTransaction = new List<InventoryTransaction>();

    public async Task<IEnumerable<InventoryTransaction>> GetInventoryTransactionAsync(string inventoryName, DateTime? dateFrom, DateTime? dateTo, InventoryTransactionType? transactionType)
    {
        var inventories = (await this.inventoryRepository.GetInventoriesByNameAsync(string.Empty)).ToList();

        var query = from it in this._inventoryTransaction
            join inv in inventories on it.InventoryID equals inv.InventoryID
            where 
            (
                string.IsNullOrWhiteSpace(inventoryName) || inv.InventoryName.ToLower().IndexOf(inventoryName.ToLower()) >= 0)
                &&
                (!dateFrom.HasValue || it.TransactionDate >= dateFrom.Value.Date) &&
                (!dateTo.HasValue || it.TransactionDate <= dateTo.Value.Date) &&
                (!transactionType.HasValue || it.InventoryTransactionType == transactionType
            )
            select new InventoryTransaction
            {
                Inventory = inv,
                InventoryTransactionID = it.InventoryTransactionID,
                PONumber = it.PONumber,
                InventoryID = it.InventoryID,
                QuantityBefore = it.QuantityBefore,
                QuantityAfter = it.QuantityAfter,
                TransactionDate = it.TransactionDate,
                DoneBy = it.DoneBy,
                UnitPrice = it.UnitPrice
            };

            return query;
    }

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