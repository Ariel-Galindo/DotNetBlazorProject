using System;
using IMS.Application.Products.Interfaces;
using IMS.Application.Reports.Interfaces;
using IMS.Domain;
using IMS.Domain.Enums;

namespace IMS.Application.Reports;

public class SearchProductTransactionUseCase(IProductTransactionRepository productTransactionRepository) : ISearchProductTransactionUseCase
{
    private readonly IProductTransactionRepository productTransactionRepository = productTransactionRepository;

    public async Task<IEnumerable<ProductTransaction>> ExecuteAsync(string inventoryName, DateTime? dateFrom, DateTime? dateTo, ProductTransactionType? transactionType)
    {
        if (dateTo.HasValue) dateTo = dateTo.Value.AddDays(1);

        return await this.productTransactionRepository.GetProductTransactionAsync(inventoryName, dateFrom, dateTo, transactionType);
    }
}
