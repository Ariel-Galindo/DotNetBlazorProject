using System;
using System.ComponentModel.DataAnnotations;
using IMS.Domain.Enums;

namespace IMS.Domain;

public class ProductTransaction
{
    public int ProductTransactionID { get; set; }
    public string SONumber { get; set; } = string.Empty;
    [Required]
    public int ProductID { get; set; }
    public string ProductionNumber { get; set; } = string.Empty;
    [Required]
    public int QuantityBefore { get; set; }
    [Required]
    public int QuantityAfter { get; set; }
    [Required]
    public ProductTransactionType ProductTransactionType { get; set; }
    public double? UnitPrice { get; set; }
    [Required]
    public DateTime TransactionDate { get; set; }
    [Required]
    public string DoneBy { get; set; } = string.Empty;
    public Product? Product { get; set; }
}
