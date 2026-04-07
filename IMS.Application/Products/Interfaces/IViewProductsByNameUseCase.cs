using System;
using IMS.Domain;

namespace IMS.Application.Products.Interfaces;

public interface IViewProductsByNameUseCase
{
    Task<IEnumerable<Product>> ExecuteAsync(string name = "");
}
