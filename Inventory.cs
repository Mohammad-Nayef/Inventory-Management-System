using System.Text;

public class Inventory
{
    private ProductsRepository _mongoDb = new ProductsRepository(Constants.MongoDbConnectionString);
    
    public async Task AddProductAsync(Product newProduct)
    {
        await _mongoDb.AddProductAsync(newProduct);
    }

    public async Task<bool> IsEmptyAsync()
    {
        return (await _mongoDb.GetAllProductsAsync()).Count == 0;
    }

    public async Task<string> PrintAllProductsAsync()
    {
        if (await IsEmptyAsync())
            return "There are no products.";

        var allProducts = new StringBuilder();
        var products = await _mongoDb.GetAllProductsAsync();

        for (var i = 0; i < products.Count; i++)
        {
            allProducts.Append($"""
                Product #{i + 1}:
                {products[i].ToString()}
                

                """);
        }

        return allProducts.ToString();
    }

    public async Task EditProductNameAsync(Product product, string? newName)
    {
        await _mongoDb.EditProductNameAsync(product.Name, newName);
    }

    public async Task EditProductPriceAsync(Product product, decimal newPrice)
    {
        await _mongoDb.EditProductPriceAsync(product.Name, newPrice);
    }

    public async Task EditProductQuantityAsync(Product product, int newQuantity)
    {
        await _mongoDb.EditProductQuantityAsync(product.Name, newQuantity);
    }

    public async Task DeleteProductAsync(Product product)
    {
        await _mongoDb.DeleteProductAsync(product.Name);
    }

    /// <summary>
    /// Returns an object of the product or null (if it's not found).
    /// </summary>
    /// <returns>Product?</returns>
    public async Task<Product?> FindProductAsync(string? productName)
    {
        return (await _mongoDb.GetAllProductsAsync())
            .SingleOrDefault(product => product.Name == productName);
    }
}