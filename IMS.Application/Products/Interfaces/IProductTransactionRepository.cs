using System;
using IMS.Domain;
using IMS.Domain.Enums;

namespace IMS.Application.Products.Interfaces;

public interface IProductTransactionRepository
{
    Task<IEnumerable<ProductTransaction>> GetProductTransactionAsync(string inventoryName, DateTime? dateFrom, DateTime? dateTo, ProductTransactionType? transactionType);
    Task ProduceAsync(string productionNumber, Product product, int quantity, string doneBy);
    Task SellProductAsync(string salesOrderNumber, Product product, int quantity, double unitPrice, string doneBy);
}
