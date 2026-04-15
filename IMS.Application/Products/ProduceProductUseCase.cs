using System;
using IMS.Application.Products.Interfaces;
using IMS.Domain;

namespace IMS.Application.Products;

public class ProduceProductUseCase(IProductTransactionRepository productTransactionRepository, IProductRepository productRepository) : IProduceProductUseCase
{
    private readonly IProductTransactionRepository productTransactionRepository = productTransactionRepository;
    private readonly IProductRepository productRepository = productRepository;

    public async Task ExecuteAsync(string productionNumber, Product product, int quantity, string doneBy)
    {
        await this.productTransactionRepository.ProduceAsync(productionNumber, product, quantity, doneBy);

        product.Quantity += quantity;
        await this.productRepository.UpdateProductAsync(product);
    }
}