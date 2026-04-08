using IMS.Application.Products.Interfaces;

namespace IMS.Application.Products;

public class DeleteProductUseCase(IProductRepository ProductRepository) : IDeleteProductUseCase
{
    private readonly IProductRepository ProductRepository = ProductRepository;

    public async Task ExecuteAsync(int ProductID)
    {
        await ProductRepository.DeleteProductByIdAsync(ProductID);
    }
}