using IMS.Application.Products.Interfaces;
using IMS.Domain;

namespace IMS.Application.Products;

public class EditProductUseCase(IProductRepository ProductRepository) : IEditProductUseCase
{
    private readonly IProductRepository ProductRepository = ProductRepository;

    public async Task ExecuteAsync(Product Product)
    {
        await ProductRepository.UpdateProductAsync(Product);
    }
}