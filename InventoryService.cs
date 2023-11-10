using System.Text;

public class InventoryService
{
    private IProductRepository _repository;

    public InventoryService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task AddProductAsync(Product newProduct)
    {
        await _repository.AddProductAsync(newProduct);
    }

    public async Task<bool> IsEmptyAsync()
    {
        return !(await _repository.GetAllProductsAsync())
            .Any();
    }

    public async Task<string> PrintAllProductsAsync()
    {
        if (await IsEmptyAsync())
            return "There are no products.";

        var allProducts = new StringBuilder();
        var products = await _repository.GetAllProductsAsync();

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
        await _repository.EditProductNameAsync(product.Name, newName);
    }

    public async Task EditProductPriceAsync(Product product, decimal newPrice)
    {
        await _repository.EditProductPriceAsync(product.Name, newPrice);
    }

    public async Task EditProductQuantityAsync(Product product, int newQuantity)
    {
        await _repository.EditProductQuantityAsync(product.Name, newQuantity);
    }

    public async Task DeleteProductAsync(Product product)
    {
        await _repository.DeleteProductAsync(product.Name);
    }

    /// <summary>
    /// Returns an object of the product or null (if it's not found).
    /// </summary>
    /// <returns>Product?</returns>
    public async Task<Product?> FindProductAsync(string productName)
    {
        return (await _repository.GetAllProductsAsync())
            .SingleOrDefault(product => product.Name == productName);
    }
}