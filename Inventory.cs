using System.Text;

public class Inventory
{
    public void AddProduct(Product newProduct)
    {
        MongoDb.Instance.AddProduct(newProduct);
    }

    public bool IsEmpty()
    {
        return MongoDb.Instance.GetAllProducts().Count == 0;
    }

    public string PrintAllProducts()
    {
        if (IsEmpty())
            return "There are no products.";

        var allProducts = new StringBuilder();
        var products = MongoDb.Instance.GetAllProducts();

        for (var i = 0; i < products.Count; i++)
        {
            allProducts.Append($"""
                Product #{i + 1}:
                {products[i].ToString()}
                

                """);
        }

        return allProducts.ToString();
    }

    public void EditProductName(Product product, string? newName)
    {
        MongoDb.Instance.EditProductName(product.Name, newName);
    }

    public void EditProductPrice(Product product, decimal newPrice)
    {
        MongoDb.Instance.EditProductPrice(product.Name, newPrice);
    }

    public void EditProductQuantity(Product product, int newQuantity)
    {
        MongoDb.Instance.EditProductQuantity(product.Name, newQuantity);
    }

    public void DeleteProduct(Product product)
    {
        MongoDb.Instance.DeleteProduct(product.Name);
    }

    /// <summary>
    /// Returns an object of the product or null (if it's not found).
    /// </summary>
    /// <returns>Product?</returns>
    public Product? FindProduct(string? productName)
    {
        return MongoDb.Instance.GetAllProducts()
            .SingleOrDefault(product => product.Name == productName);
    }
}