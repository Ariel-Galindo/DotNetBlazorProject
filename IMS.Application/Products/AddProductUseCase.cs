using IMS.Application.Products.Interfaces;
using IMS.Domain;

namespace IMS.Application.Inventories;

public class AddProductUseCase(IProductRepository productRepository) : IAddProductUseCase
{
    private readonly IProductRepository productRepository = productRepository;

    public async Task ExecuteAsync(Product Product)
    {
        await productRepository.AddProductAsync(Product);
    }
}