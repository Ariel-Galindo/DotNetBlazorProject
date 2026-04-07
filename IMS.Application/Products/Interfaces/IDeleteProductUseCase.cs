using System;

namespace IMS.Application.Products.Interfaces;

public interface IDeleteProductUseCase
{
    Task ExecuteAsync(int ProductID);
}
