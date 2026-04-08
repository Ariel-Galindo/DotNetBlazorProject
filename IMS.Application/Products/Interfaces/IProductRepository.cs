using System;
using IMS.Domain;

namespace IMS.Application.Products.Interfaces;

public interface IProductRepository
{
    Task AddProductAsync(Product product);
    Task DeleteProductByIdAsync(int productID);
    Task<IEnumerable<Product>> GetProductsByNameAsync(string name);
    Task<Product?> GetProductByIdAsync(int productID);
    Task UpdateProductAsync(Product product);
}
