using System;
using IMS.Application.Activities.Interfaces;
using IMS.Application.Products.Interfaces;
using IMS.Domain.Enums;
using IMS.Domain;
using IMS.Application.Inventories.Interfaces;

namespace IMS.InMemory;

public class ProductTransactionRepository(IProductRepository productRepository, IInventoryTransactionRepository iventoryTransactionRepository, IInventoryRepository inventoryRepository) : IProductTransactionRepository
{
    private List<ProductTransaction> _productTransaction = new();
    private readonly IProductRepository productRepository = productRepository;
    private readonly IInventoryTransactionRepository iventoryTransactionRepository = iventoryTransactionRepository;
    private readonly IInventoryRepository inventoryRepository = inventoryRepository;

    public async Task ProduceAsync(string productionNumber, Product product, int quantityToConsume, string doneBy)
    {
        var prod = await this.productRepository.GetProductByIdAsync(product.ProductID);

        if (prod != null)
        {
            foreach (var item in prod.ProductInventories)
            {
                if (item.Inventory != null)
                {
                    this.iventoryTransactionRepository.ProduceAsync(
                        productionNumber,
                        item.Inventory,
                        item.Inventory!.Quantity * quantityToConsume,
                        doneBy,
                         -1);

                    var inv = await this.inventoryRepository.GetInventoryByIdAsync(item.InventoryID);
                    inv!.Quantity -= item.InventoryQuantity * quantityToConsume;
                    await this.inventoryRepository.UpdateInventoryAsync(inv);
                }
            }
        }

        this._productTransaction.Add(new ProductTransaction
        {
            ProductionNumber = productionNumber,
            ProductID = product.ProductID,
            QuantityBefore = product.Quantity,
            QuantityAfter = product.Quantity + quantityToConsume,
            ProductTransactionType = ProductTransactionType.ProduceProduct,
            TransactionDate = DateTime.Now,
            DoneBy = doneBy
        });
    }

    public Task SellProductAsync(string salesOrderNumber, Product product, int quantity, double unitPrice, string doneBy)
    {
        this._productTransaction.Add(new ProductTransaction
        {
            ProductTransactionType = ProductTransactionType.SellProduct,
            SONumber = salesOrderNumber,
            ProductID = product.ProductID,
            QuantityBefore = product.Quantity,
            QuantityAfter = product.Quantity - quantity,
            TransactionDate = DateTime.Now,
            UnitPrice = product.Price
        });

        return Task.CompletedTask;
    }

    public async Task<IEnumerable<ProductTransaction>> GetProductTransactionAsync(string productName, DateTime? dateFrom, DateTime? dateTo, ProductTransactionType? transactionType)
    {
        var products = (await this.productRepository.GetProductsByNameAsync(string.Empty)).ToList();

        var query = from it in this._productTransaction
            join pro in products on it.ProductID equals pro.ProductID
            where 
            (
                string.IsNullOrWhiteSpace(productName) || pro.ProductName.ToLower().IndexOf(productName.ToLower()) >= 0)
                &&
                (!dateFrom.HasValue || it.TransactionDate >= dateFrom.Value.Date) &&
                (!dateTo.HasValue || it.TransactionDate <= dateTo.Value.Date) &&
                (!transactionType.HasValue || it.ProductTransactionType == transactionType
            )
            select new ProductTransaction
            {
                Product = pro,
                ProductTransactionID = it.ProductTransactionID,
                SONumber = it.SONumber,
                ProductID = it.ProductID,
                QuantityBefore = it.QuantityBefore,
                QuantityAfter = it.QuantityAfter,
                ProductTransactionType = it.ProductTransactionType,
                TransactionDate = it.TransactionDate,
                DoneBy = it.DoneBy,
                UnitPrice = it.UnitPrice
            };

            return query;
    }
}
