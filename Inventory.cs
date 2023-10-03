using System.Text;

public class Inventory
{
    private List<Product> _products = new();

    public void AddProduct(Product newProduct)
    {
        ProductsDatabase.Instance.AddProduct(newProduct);
    }

    public bool IsEmpty()
    {
        return _products.Count == 0;
    }

    public string PrintAllProducts()
    {
        if (IsEmpty())
            return "There are no products.";

        var allProducts = new StringBuilder();

        for (var i = 0; i < _products.Count; i++)
        {
            allProducts.Append($"""
                Product #{i + 1}:
                {_products[i].ToString()}
                

                """);
        }

        return allProducts.ToString();
    }

    public void EditProductName(Product product, string? newName)
    {
        product.Name = newName;
    }

    public void EditProductPrice(Product product, decimal newPrice)
    {
        product.Price = newPrice;
    }

    public void EditProductQuantity(Product product, int newQuantity)
    {
        product.Quantity = newQuantity;
    }

    public void DeleteProduct(Product product)
    {
        _products.Remove(product);
    }

    /// <summary>
    /// Returns an object of the product or null (if it's not found).
    /// </summary>
    /// <returns>Product?</returns>
    public Product? FindProduct(string? productName)
    {
        foreach (var product in _products)
        {
            if (product.Name.Equals(productName, StringComparison.InvariantCultureIgnoreCase))
                return product;
        }
        return null;
    }
}