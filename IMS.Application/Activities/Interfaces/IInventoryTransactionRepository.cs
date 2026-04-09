using System;
using IMS.Domain;

namespace IMS.Application.Activities.Interfaces;

public interface IInventoryTransactionRepository
{
    void PurchaseAsync(string poNumber, Inventory inventory, int quantity, string doneBy, double price);
}
