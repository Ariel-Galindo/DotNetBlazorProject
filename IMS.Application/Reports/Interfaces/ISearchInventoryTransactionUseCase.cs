using System;
using IMS.Domain;
using IMS.Domain.Enums;

namespace IMS.Application.Reports.Interfaces;

public interface ISearchInventoryTransactionUseCase
{
    Task<IEnumerable<InventoryTransaction>> ExecuteAsync(string inventoryName, DateTime? dateFrom, DateTime? dateTo, InventoryTransactionType? transactionType);
}
