using System;
using IMS.Domain;

namespace IMS.Application.Interfaces;

public interface IViewInventoriesByNameUseCase
{
    Task<IEnumerable<Inventory>> ExecuteAsync(string name = "");
}
