using MongoDB.Driver;

public class MongoDb
{
    private static readonly Lazy<MongoDb> _lazyInstance = new Lazy<MongoDb>(() => new MongoDb());
    private const string connectionString = "mongodb+srv://moha:Yr2Ab6A2lVO00J0A@cluster0.j4olwzd.mongodb.net/?retryWrites=true&w=majority";
    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<Product> productsCollection;

    public static MongoDb Instance => _lazyInstance.Value;

    private MongoDb()
    {
        client = new MongoClient(connectionString);
        database = client.GetDatabase("inventory");
        productsCollection = database.GetCollection<Product>("products");
    }

    public void AddProduct(Product newProduct)
    {
        productsCollection.InsertOneAsync(newProduct);
    }

    public void EditProductName(string oldName, string newName)
    {
        var nameUpdate = Builders<Product>.Update.Set("Name", newName);
        productsCollection.FindOneAndUpdateAsync(product => product.Name == oldName, nameUpdate);
    }

    public void EditProductPrice(string productName, decimal newPrice)
    {
        var priceUpdate = Builders<Product>.Update.Set("Price", newPrice);
        productsCollection.FindOneAndUpdateAsync(product => product.Name == productName, priceUpdate);
    }

    public void EditProductQuantity(string productName, int newQuantity)
    {
        var quantityUpdate = Builders<Product>.Update.Set("Quantity", newQuantity);
        productsCollection.FindOneAndUpdateAsync(product => product.Name == productName, quantityUpdate);
    }

    public void DeleteProduct(string productName)
    {
        productsCollection.DeleteOneAsync(product => product.Name == productName);
    }
}