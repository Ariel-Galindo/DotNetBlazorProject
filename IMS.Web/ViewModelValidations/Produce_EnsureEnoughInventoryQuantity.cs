using System;
using System.ComponentModel.DataAnnotations;
using IMS.Web.ViewModels;

namespace IMS.Web.ViewModelValidations;

public class Produce_EnsureEnoughInventoryQuantity : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var produceViewModel = validationContext.ObjectInstance as ProduceViewModel;

        if (produceViewModel != null)
        {
            if (produceViewModel.Product != null)
            {
                foreach (var item in produceViewModel.Product.ProductInventories)
                {
                    if (item is not null && item.InventoryQuantity * produceViewModel.QuantityToProduce > item.Inventory!.Quantity)
                    {
                        return new ValidationResult($"The inventory ({item.Inventory.InventoryName}) is not enough to procuce {produceViewModel.QuantityToProduce} products",
                            new [] { validationContext.MemberName! });
                    }
                }
            }
        }

        return ValidationResult.Success;
    }
}
