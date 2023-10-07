using System.Text;

public class Inventory
{
    public async Task AddProductAsync(Product newProduct)
    {
        await MongoDb.Instance.AddProductAsync(newProduct);
    }

    public async Task<bool> IsEmptyAsync()
    {
        return (await MongoDb.Instance.GetAllProductsAsync()).Count == 0;
    }

    public async Task<string> PrintAllProductsAsync()
    {
        if (await IsEmptyAsync())
            return "There are no products.";

        var allProducts = new StringBuilder();
        var products = await MongoDb.Instance.GetAllProductsAsync();

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
        await MongoDb.Instance.EditProductNameAsync(product.Name, newName);
    }

    public async Task EditProductPriceAsync(Product product, decimal newPrice)
    {
        await MongoDb.Instance.EditProductPriceAsync(product.Name, newPrice);
    }

    public async Task EditProductQuantityAsync(Product product, int newQuantity)
    {
        await MongoDb.Instance.EditProductQuantityAsync(product.Name, newQuantity);
    }

    public async Task DeleteProductAsync(Product product)
    {
        await MongoDb.Instance.DeleteProductAsync(product.Name);
    }

    /// <summary>
    /// Returns an object of the product or null (if it's not found).
    /// </summary>
    /// <returns>Product?</returns>
    public async Task<Product?> FindProductAsync(string? productName)
    {
        return (await MongoDb.Instance.GetAllProductsAsync())
            .SingleOrDefault(product => product.Name == productName);
    }
}