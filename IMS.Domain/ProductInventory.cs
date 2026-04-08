using System;
using System.Text.Json.Serialization;

namespace IMS.Domain;

public class ProductInventory
{
    public int ProductID { get; set; }

    [JsonIgnore]
    public Product? Product { get; set; }
    public int InventoryID { get; set; }

    [JsonIgnore]
    public Inventory? Inventory { get; set; }
    public int InventoryQuantity { get; set; }
}
