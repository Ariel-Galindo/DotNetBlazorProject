using System;
using System.ComponentModel.DataAnnotations;

namespace IMS.Domain;

public class Product
{
    public int ProductID { get; set; }

    [Required]
    [StringLength(150)]
    public string ProductName { get; set; } = string.Empty;

    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater or equal to 0.")]
    public int Quantity { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Price must be greater or equal to 0.")]
    public double Price { get; set; }

    public List<ProductInventory> ProductInventories { get; set; } = new();

    public void AddInventory(Inventory inventory)
    {
        if (this.ProductInventories.Any(x => x.Inventory is not null &&
        x.Inventory.InventoryName.Equals(inventory.InventoryName, StringComparison.OrdinalIgnoreCase)))
        {
            this.ProductInventories.Add(new ProductInventory
            {
                InventoryID = inventory.InventoryID,
                Inventory = inventory,
                InventoryQuantity = inventory.Quantity,
                ProductID = this.ProductID,
                Product = this
            });
        }
    }
}
