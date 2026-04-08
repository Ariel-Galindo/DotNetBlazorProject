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

    public Task AddProductAsync(Product product)
    {
        if (_products.Any(x => x.ProductName.Equals(product.ProductName, StringComparison.OrdinalIgnoreCase))) return Task.CompletedTask;

        var maxId = _products.Max(x => x.ProductID);
        product.ProductID = maxId + 1;

        _products.Add(product);

        return Task.CompletedTask;
    }

    public Task DeleteProductByIdAsync(int productID)
    {
        var Product = _products.FirstOrDefault(x => x.ProductID == productID);
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

    public async Task<Product?> GetProductByIdAsync(int productID)
    {
        var product = _products.FirstOrDefault(x => x.ProductID == productID);
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

    public Task UpdateProductAsync(Product product)
    {
        if (_products.Any(x => x.ProductID != product.ProductID &&
        x.ProductName.Equals(product.ProductName, StringComparison.OrdinalIgnoreCase)))
            return Task.CompletedTask;

        var proToUpdate = _products.FirstOrDefault(x => x.ProductID == product.ProductID);
        if (proToUpdate != null)
        {
            proToUpdate.ProductName = product.ProductName;
            proToUpdate.Quantity = product.Quantity;
            proToUpdate.Price = product.Price;
            proToUpdate.ProductInventories = product.ProductInventories;
        }

        return Task.CompletedTask;
    }
}