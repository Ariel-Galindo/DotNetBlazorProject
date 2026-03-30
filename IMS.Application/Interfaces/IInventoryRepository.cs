using System;
using IMS.Domain;

namespace IMS.Application.Interfaces;

public interface IInventoryRepository
{
    Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name);
}
