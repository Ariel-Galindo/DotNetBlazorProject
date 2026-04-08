using System;
using IMS.Domain;

namespace IMS.Application.Products.Interfaces;

public interface IViewProductByIdUseCase
{
    Task<Product?> ExecuteAsync(int ProductID);
}
