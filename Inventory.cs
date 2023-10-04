using System.Text;

public class Inventory
{
    public void AddProduct(Product newProduct)
    {
        SqlServerDb.Instance.AddProduct(newProduct);
    }

    public bool IsEmpty()
    {
        return SqlServerDb.Instance.GetAllProducts().Count == 0;
    }

    public string PrintAllProducts()
    {
        if (IsEmpty())
            return "There are no products.";

        var allProducts = new StringBuilder();
        var products = SqlServerDb.Instance.GetAllProducts();

        for (var i = 0; i < products.Count; i++)
        {
            allProducts.Append($"""
                Product #{i + 1}:
                {products[i].ToString()}
                

                """);
        }

        return allProducts.ToString();
    }

    public void EditProductName(Product product, string newName)
    {
        SqlServerDb.Instance.EditProductName(product.Name, newName);
    }

    public void EditProductPrice(Product product, decimal newPrice)
    {
        SqlServerDb.Instance.EditProductPrice(product.Name, newPrice);
    }

    public void EditProductQuantity(Product product, int newQuantity)
    {
        SqlServerDb.Instance.EditProductQuantity(product.Name, newQuantity);
    }

    public void DeleteProduct(Product product)
    {
        SqlServerDb.Instance.DeleteProduct(product.Name);
    }

    /// <summary>
    /// Returns an object of the product or null (if it's not found).
    /// </summary>
    /// <returns>Product?</returns>
    public Product? FindProduct(string productName)
    {
        return SqlServerDb.Instance.GetAllProducts()
            .SingleOrDefault(product => product.Name == productName);
    }
}