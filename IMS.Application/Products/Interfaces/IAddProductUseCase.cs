using System;
using IMS.Domain;

namespace IMS.Application.Products.Interfaces;

public interface IAddProductUseCase
{
    Task ExecuteAsync(Product Product);
}
