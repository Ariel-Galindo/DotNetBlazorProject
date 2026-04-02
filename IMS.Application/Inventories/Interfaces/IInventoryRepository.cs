using System;
using IMS.Domain;

namespace IMS.Application.Inventories.Interfaces;

public interface IInventoryRepository
{
    Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name);
}
