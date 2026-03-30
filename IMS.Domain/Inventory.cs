namespace IMS.Domain;

public class Inventory
{
    public int InventoryID { get; set; }
    public string InventoryName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public double Price { get; set; }
}
