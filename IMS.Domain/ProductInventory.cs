using System;

namespace IMS.Domain;

public class ProductInventory
{
    public int ProductID { get; set; }
    public Product? Product { get; set; }
    public int InventoryID { get; set; }
    public Inventory? Inventory { get; set; }
    public int InventoryQuantity { get; set; }
}
