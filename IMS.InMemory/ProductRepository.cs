using IMS.Application.Products.Interfaces;
using IMS.Domain;

namespace IMS.InMemory;

public class ProductRepository : IProductRepository
{
    private readonly List<Product> _products;

    public ProductRepository()
    {
        _products = [
            new Product { ProductID = 1, ProductName = "Bike", Quantity = 10, Price = 200 },
            new Product { ProductID = 2, ProductName = "Car", Quantity = 20, Price = 400 },
        ];
    }

    public Task AddProductAsync(Product Product)
    {
        if (_products.Any(x => x.ProductName.Equals(Product.ProductName, StringComparison.OrdinalIgnoreCase))) return Task.CompletedTask;

        var maxId = _products.Max(x => x.ProductID);
        Product.ProductID = maxId + 1;

        _products.Add(Product);

        return Task.CompletedTask;
    }

    public Task DeleteProductByIdAsync(int ProductID)
    {
        var Product = _products.FirstOrDefault(x => x.ProductID == ProductID);
        if (Product is not null)
        {
            _products.Remove(Product);
        }

        return Task.CompletedTask;
    }

    public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return await Task.FromResult(_products);

        return _products.Where(x => x.ProductName.Contains(name, StringComparison.CurrentCultureIgnoreCase));
    }

    public async Task<Product?> GetProductByIdAsync(int ProductID)
    {
        var product = _products.FirstOrDefault(x => x.ProductID == ProductID);
        var newProduct = new Product();

        if (product != null)
        {
            newProduct.ProductID = product.ProductID;
            newProduct.ProductName = product.ProductName;
            newProduct.Price = product.Price;
            newProduct.Quantity = product.Quantity;
            newProduct.ProductInventories = new List<ProductInventory>();
            if (product.ProductInventories != null && product.ProductInventories.Count > 0)
            {
                foreach (var prodInv in product.ProductInventories)
                {
                    var newProdInv = new ProductInventory
                    {
                        InventoryID = prodInv.InventoryID,
                        ProductID = prodInv.ProductID,
                        Product = product,
                        Inventory = new Inventory(),
                        InventoryQuantity = prodInv.InventoryQuantity
                    };
                    if (prodInv.Inventory != null)
                    {
                        newProdInv.Inventory.InventoryID = prodInv.Inventory.InventoryID;
                        newProdInv.Inventory.InventoryName = prodInv.Inventory.InventoryName;
                        newProdInv.Inventory.Price = prodInv.Inventory.Price;
                        newProdInv.Inventory.Quantity = prodInv.Inventory.Quantity;
                    }

                    newProduct.ProductInventories.Add(newProdInv);
                }
            }
        }

        return await Task.FromResult(newProduct);
    }

    public Task UpdateProductAsync(Product Product)
    {
        if (_products.Any(x => x.ProductID != Product.ProductID &&
        x.ProductName.Equals(Product.ProductName, StringComparison.OrdinalIgnoreCase)))
            return Task.CompletedTask;

        var invToUpdate = _products.FirstOrDefault(x => x.ProductID == Product.ProductID);
        if (invToUpdate is not null)
        {
            invToUpdate.ProductName = Product.ProductName;
            invToUpdate.Quantity = Product.Quantity;
            invToUpdate.Price = Product.Price;
        }

        return Task.CompletedTask;
    }
}