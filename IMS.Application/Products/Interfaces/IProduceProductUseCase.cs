using System;
using IMS.Domain;

namespace IMS.Application.Products.Interfaces;

public interface IProduceProductUseCase
{
    Task ExecuteAsync(string productionNumber, Product product, int quantity, string doneBy);
}
