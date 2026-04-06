using System;

namespace IMS.Application.Inventories.Interfaces;

public interface IDeleteInventoryUseCase
{
    Task ExecuteAsync(int inventoryID);
}
