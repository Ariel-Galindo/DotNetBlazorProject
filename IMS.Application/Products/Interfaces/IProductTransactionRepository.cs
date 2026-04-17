using System;
using IMS.Domain;

namespace IMS.Application.Products.Interfaces;

public interface IProductTransactionRepository
{
    Task ProduceAsync(string productionNumber, Product product, int quantity, string doneBy);
    Task SellProductAsync(string salesOrderNumber, Product product, int quantity, string doneBy);
}
