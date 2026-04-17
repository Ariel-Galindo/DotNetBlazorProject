using System;
using IMS.Application.Activities.Interfaces;
using IMS.Application.Products.Interfaces;
using IMS.Domain;

namespace IMS.Application.Activities;

public class SellProductUseCase(IProductTransactionRepository productTransactionRepository, IProductRepository productRepository) : ISellProductUseCase
{
    private readonly IProductTransactionRepository productTransactionRepository = productTransactionRepository;
    private readonly IProductRepository productRepository = productRepository;

    public async Task ExecuteAsync(string salesOrderNumber, Product product, int quantity, string doneBy)
    {
        await this.productTransactionRepository.SellProductAsync(salesOrderNumber, product, quantity, doneBy);

        product.Quantity -= quantity;
        await this.productRepository.UpdateProductAsync(product);
    }
}