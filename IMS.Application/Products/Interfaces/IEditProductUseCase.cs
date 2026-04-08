using System;
using IMS.Domain;

namespace IMS.Application.Products.Interfaces;

public interface IEditProductUseCase
{
    Task ExecuteAsync(Product Product);
}
