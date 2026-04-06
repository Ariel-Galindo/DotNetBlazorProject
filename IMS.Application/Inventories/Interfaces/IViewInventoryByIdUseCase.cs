using System;
using IMS.Domain;

namespace IMS.Application.Inventories.Interfaces;

public interface IViewInventoryByIdUseCase
{
    Task<Inventory> ExecuteAsync(int inventoryID);
}
