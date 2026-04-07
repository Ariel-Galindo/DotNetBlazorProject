using System;
using IMS.Domain;

namespace IMS.Application.Products.Interfaces;

public interface IProductRepository
{
    Task AddProductAsync(Product Product);
    Task DeleteProductByIdAsync(int ProductID);
    Task<IEnumerable<Product>> GetProductsByNameAsync(string name);
    Task<Product?> GetProductByIdAsync(int ProductID);
    Task UpdateProductAsync(Product Product);
}
