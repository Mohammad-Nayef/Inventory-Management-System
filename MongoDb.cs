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
}