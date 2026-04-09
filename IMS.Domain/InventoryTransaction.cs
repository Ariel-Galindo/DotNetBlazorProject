using System;
using System.ComponentModel.DataAnnotations;
using IMS.Domain.Enums;

namespace IMS.Domain;

public class InventoryTransaction
{
    public int InventoryTransactionID { get; set; }
    [Required]
    public int InventoryID { get; set; }
    public string PONumber { get; set; } = string.Empty;
    [Required]
    public int QuantityBefore { get; set; }
    [Required]
    public int QuantityAfter { get; set; }
    [Required]
    public InventoryTransactionType InventoryTransactionType { get; set; }
    public double UnitPrice { get; set; }
    [Required]
    public DateTime TransactionDate { get; set; }
    [Required]
    public string DoneBy { get; set; } = string.Empty;
    public Inventory? Inventory { get; set; }
}
