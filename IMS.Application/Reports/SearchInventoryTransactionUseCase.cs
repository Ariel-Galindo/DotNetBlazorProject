using System;
using IMS.Application.Activities.Interfaces;
using IMS.Application.Reports.Interfaces;
using IMS.Domain;
using IMS.Domain.Enums;

namespace IMS.Application.Reports;

public class SearchInventoryTransactionUseCase(IInventoryTransactionRepository inventoryTransactionRepository) : ISearchInventoryTransactionUseCase
{
    private readonly IInventoryTransactionRepository inventoryTransactionRepository = inventoryTransactionRepository;

    public async Task<IEnumerable<InventoryTransaction>> ExecuteAsync(string inventoryName, DateTime? dateFrom, DateTime? dateTo, InventoryTransactionType? transactionType)
    {
        if (dateTo.HasValue) dateTo = dateTo.Value.AddDays(1);
        
        return await this.inventoryTransactionRepository.GetInventoryTransactionAsync(inventoryName, dateFrom, dateTo, transactionType);
    }
}