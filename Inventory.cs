using System.Text;

public class Inventory
{
    private SqlServerDb database = new SqlServerDb(Constants.SqlServerConnectionString);

    public async Task AddProductAsync(Product newProduct)
    {
        await database.AddProductAsync(newProduct);
    }

    public async Task<bool> IsEmptyAsync()
    {
        return !(await database.GetAllProductsAsync())
            .Any();
    }

    public async Task<string> PrintAllProductsAsync()
    {
        if (await IsEmptyAsync())
            return "There are no products.";

        var allProducts = new StringBuilder();
        var products = await database.GetAllProductsAsync();

        for (var i = 0; i < products.Count; i++)
        {
            allProducts.Append($"""
                Product #{i + 1}:
                {products[i].ToString()}
                

                """);
        }

        return allProducts.ToString();
    }

    public async Task EditProductNameAsync(Product product, string newName)
    {
        await database.EditProductNameAsync(product.Name, newName);
    }

    public async Task EditProductPriceAsync(Product product, decimal newPrice)
    {
        await database.EditProductPriceAsync(product.Name, newPrice);
    }

    public async Task EditProductQuantityAsync(Product product, int newQuantity)
    {
        await database.EditProductQuantityAsync(product.Name, newQuantity);
    }

    public async Task DeleteProductAsync(Product product)
    {
        await database.DeleteProductAsync(product.Name);
    }

    /// <summary>
    /// Returns an object of the product or null (if it's not found).
    /// </summary>
    /// <returns>Product?</returns>
    public async Task<Product?> FindProductAsync(string productName)
    {
        return (await database.GetAllProductsAsync())
            .SingleOrDefault(product => product.Name == productName);
    }
}