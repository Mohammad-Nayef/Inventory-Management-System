using System.Data.SqlClient;

public class SqlServerDb
{
    private static readonly Lazy<SqlServerDb> _lazyInstance = new Lazy<SqlServerDb>(() => new SqlServerDb());
    private static SqlConnection sqlConnection;
    private string connectionString = "Data Source = MOHAMMAD; " +
        "Initial Catalog = Inventory; " +
        "Integrated Security = True;";

    public static SqlServerDb Instance => _lazyInstance.Value;

    private SqlServerDb()
    {
        InitializeDatabaseAsync().Wait();
    }

    private async Task InitializeDatabaseAsync()
    {
        sqlConnection = new SqlConnection(connectionString);
        await CreateProductsTableAsync();
    }

    private async Task CreateProductsTableAsync()
    {
        var query = """
                IF object_id('Products') IS NULL
                BEGIN
                    CREATE TABLE Products (
                	    name VARCHAR(100),
                        price FLOAT,
                        quantity INT
                    )
                END
                """;

        await ExecuteQueryAsync(query);
    }

    public async Task AddProductAsync(Product newProduct)
    {
        var query = $"""
                INSERT INTO Products
                VALUES ({newProduct.Name}, {newProduct.Price}, {newProduct.Quantity})
                """;

        await ExecuteQueryAsync(query);
    }

    public async Task EditProductNameAsync(string oldName, string newName)
    {
        var query = $"""
                UPDATE Products
                SET name = '{newName}'
                WHERE name = '{oldName}'
                """;

        await ExecuteQueryAsync(query);
    }

    public async Task EditProductPriceAsync(string productName, decimal newPrice)
    {
        var query = $"""
                UPDATE Products
                SET price = {newPrice}
                WHERE name = '{productName}'
                """;

        await ExecuteQueryAsync(query);
    }

    public async Task EditProductQuantityAsync(string productName, int newQuantity)
    {
        var query = $"""
                UPDATE Products
                SET quantity = {newQuantity}
                WHERE name = '{productName}'
                """;

        await ExecuteQueryAsync(query);
    }

    public async Task DeleteProductAsync(string productName)
    {
        var query = $"""
                DELETE FROM Products
                WHERE name = '{productName}'
                """;

        await ExecuteQueryAsync(query);
    }

    /// <summary>
    /// Opens database connection then executes the query and closes the connection.
    /// </summary>
    private async Task ExecuteQueryAsync(string query)
    {
        await sqlConnection.OpenAsync();
        var sqlCommand = new SqlCommand(query, sqlConnection);
        await sqlCommand.ExecuteNonQueryAsync();
        await sqlConnection.CloseAsync();
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        var query = "SELECT * FROM Products";

        await sqlConnection.OpenAsync();
        var sqlCommand = new SqlCommand(query, sqlConnection);
        var products = new List<Product>();

        using (var productsDataReader = sqlCommand.ExecuteReader())
        {
            while (productsDataReader.Read())
            {
                products.Add(GetProductFromString(productsDataReader));
            }
        }

        await sqlConnection.CloseAsync();
        return products;
    }

    private static Product GetProductFromString(SqlDataReader productsDataReader)
    {
        var name = productsDataReader.GetString(0);
        var price = (decimal)productsDataReader.GetDouble(1);
        var quantity = productsDataReader.GetInt32(2);

        return new Product(name, price, quantity);
    }
}