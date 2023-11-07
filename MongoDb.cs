using MongoDB.Driver;

public class MongoDb
{
    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<Product> productsCollection;

    public MongoDb(string connectionString)
    {
        client = new MongoClient(connectionString);
        database = client.GetDatabase("inventory");
        productsCollection = database.GetCollection<Product>("products");
    }

    public async Task AddProductAsync(Product newProduct)
    {
        await productsCollection.InsertOneAsync(newProduct);
    }

    public async Task EditProductNameAsync(string oldName, string newName)
    {
        var nameUpdate = Builders<Product>.Update.Set("Name", newName);
        await productsCollection.FindOneAndUpdateAsync(product => product.Name == oldName, nameUpdate);
    }

    public async Task EditProductPriceAsync(string productName, decimal newPrice)
    {
        var priceUpdate = Builders<Product>.Update.Set("Price", newPrice);
        await productsCollection.FindOneAndUpdateAsync(product => product.Name == productName, priceUpdate);
    }

    public async Task EditProductQuantityAsync(string productName, int newQuantity)
    {
        var quantityUpdate = Builders<Product>.Update.Set("Quantity", newQuantity);
        await productsCollection.FindOneAndUpdateAsync(product => product.Name == productName, quantityUpdate);
    }

    public async Task DeleteProductAsync(string productName)
    {
        await productsCollection.DeleteOneAsync(product => product.Name == productName);
    }

    public async Task<List<Product>> GetAllProductsAsync() => (await productsCollection.FindAsync(_ => true)).ToList();
}