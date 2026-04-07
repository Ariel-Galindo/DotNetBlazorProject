using IMS.Application.Products.Interfaces;
using IMS.Domain;

namespace IMS.Application.Products;

public class ViewProductsByNameUseCase(IProductRepository productRepository) : IViewProductsByNameUseCase
{
    private readonly IProductRepository productRepository = productRepository;

    public async Task<IEnumerable<Product>> ExecuteAsync(string name)
    {
        return await productRepository.GetProductsByNameAsync(name);
    }
}