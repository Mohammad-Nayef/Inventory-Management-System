using System.Text;

public class Inventory
{
    private MongoDb mongoDb = new MongoDb(Constants.MongoDbConnectionString);
    
    public async Task AddProductAsync(Product newProduct)
    {
        await mongoDb.AddProductAsync(newProduct);
    }

    public async Task<bool> IsEmptyAsync()
    {
        return (await mongoDb.GetAllProductsAsync()).Count == 0;
    }

    public async Task<string> PrintAllProductsAsync()
    {
        if (await IsEmptyAsync())
            return "There are no products.";

        var allProducts = new StringBuilder();
        var products = await mongoDb.GetAllProductsAsync();

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
        await mongoDb.EditProductNameAsync(product.Name, newName);
    }

    public async Task EditProductPriceAsync(Product product, decimal newPrice)
    {
        await mongoDb.EditProductPriceAsync(product.Name, newPrice);
    }

    public async Task EditProductQuantityAsync(Product product, int newQuantity)
    {
        await mongoDb.EditProductQuantityAsync(product.Name, newQuantity);
    }

    public async Task DeleteProductAsync(Product product)
    {
        await mongoDb.DeleteProductAsync(product.Name);
    }

    /// <summary>
    /// Returns an object of the product or null (if it's not found).
    /// </summary>
    /// <returns>Product?</returns>
    public async Task<Product?> FindProductAsync(string? productName)
    {
        return (await mongoDb.GetAllProductsAsync())
            .SingleOrDefault(product => product.Name == productName);
    }
}