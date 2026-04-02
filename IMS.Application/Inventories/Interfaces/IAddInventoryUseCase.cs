using System;
using IMS.Domain;

namespace IMS.Application.Inventories.Interfaces;

public interface IAddInventoryUseCase
{
    Task ExecuteAsync(Inventory inventory);
}
