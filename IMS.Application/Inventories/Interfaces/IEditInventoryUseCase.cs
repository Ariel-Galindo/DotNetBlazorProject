using System;
using IMS.Domain;

namespace IMS.Application.Inventories.Interfaces;

public interface IEditInventoryUseCase
{
    Task ExecuteAsync(Inventory inventory);
}
