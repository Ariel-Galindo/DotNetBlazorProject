using System;
using System.ComponentModel.DataAnnotations;

namespace IMS.Web.ViewModels;

public class ProduceViewModel
{
    [Required]
    public string ProductionNumber { get; set; } = string.Empty;
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "You have to select an inventory.")]
    public int ProductID { get; set; }
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Quantity has to be greater or equal to 1.")]
    public int QuantityToProduce { get; set; }
}
