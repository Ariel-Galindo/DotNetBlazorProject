using System;
using IMS.Domain;
using IMS.Domain.Enums;

namespace IMS.Application.Reports.Interfaces;

public interface ISearchProductTransactionUseCase
{
    Task<IEnumerable<ProductTransaction>> ExecuteAsync(string inventoryName, DateTime? dateFrom, DateTime? dateTo, ProductTransactionType? transactionType);
}
