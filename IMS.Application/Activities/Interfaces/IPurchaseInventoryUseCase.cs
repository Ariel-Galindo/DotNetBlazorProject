using IMS.Domain;

namespace IMS.Application.Activities.Interfaces;

public interface IPurchaseInventoryUseCase
{
    Task ExecuteAsync(string poNumber, Inventory inventory, int quantity, string doneBy, double price);
}
