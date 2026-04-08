using IMS.Application.Products.Interfaces;
using IMS.Domain;

namespace IMS.Application.Products;

public class ViewProductByIdUseCase(IProductRepository ProductRepository) : IViewProductByIdUseCase
{
    private readonly IProductRepository ProductRepository = ProductRepository;

    public async Task<Product?> ExecuteAsync(int ProductID)
    {
        return await ProductRepository.GetProductByIdAsync(ProductID);
    }
}