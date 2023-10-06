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

        await sqlConnection.OpenAsync();

        using (var sqlCommand = new SqlCommand(query, sqlConnection))
        {
            await sqlCommand.ExecuteNonQueryAsync();
        }

        await sqlConnection.CloseAsync();
    }

    public async Task AddProductAsync(Product newProduct)
    {
        var query = $"""
                INSERT INTO Products
                VALUES (@{nameof(newProduct.Name)}, @{nameof(newProduct.Price)}, 
                @{nameof(newProduct.Quantity)})
                """;

        await sqlConnection.OpenAsync();

        using (var sqlCommand = new SqlCommand(query, sqlConnection))
        {
            sqlCommand.Parameters.AddWithValue($"@{nameof(newProduct.Name)}", newProduct.Name);
            sqlCommand.Parameters.AddWithValue($"@{nameof(newProduct.Price)}", newProduct.Price);
            sqlCommand.Parameters.AddWithValue($"@{nameof(newProduct.Quantity)}", newProduct.Quantity);
            await sqlCommand.ExecuteNonQueryAsync();
        }

        await sqlConnection.CloseAsync();
    }

    public async Task EditProductNameAsync(string oldName, string newName)
    {
        var query = $"""
                UPDATE Products
                SET name = @{nameof(newName)}
                WHERE name = @{nameof(oldName)}
                """;

        await sqlConnection.OpenAsync();

        using (var sqlCommand = new SqlCommand(query, sqlConnection))
        {
            sqlCommand.Parameters.AddWithValue($"@{nameof(newName)}", newName);
            sqlCommand.Parameters.AddWithValue($"@{nameof(oldName)}", oldName);
            await sqlCommand.ExecuteNonQueryAsync();
        }

        await sqlConnection.CloseAsync();
    }

    public async Task EditProductPriceAsync(string productName, decimal newPrice)
    {
        var query = $"""
                UPDATE Products
                SET price = @{nameof(newPrice)}
                WHERE name = @{nameof(productName)}
                """;

        await sqlConnection.OpenAsync();

        using (var sqlCommand = new SqlCommand(query, sqlConnection))
        {
            sqlCommand.Parameters.AddWithValue($"@{nameof(newPrice)}", newPrice);
            sqlCommand.Parameters.AddWithValue($"@{nameof(productName)}", productName);
            await sqlCommand.ExecuteNonQueryAsync();
        }

        await sqlConnection.CloseAsync();
    }

    public async Task EditProductQuantityAsync(string productName, int newQuantity)
    {
        var query = $"""
                UPDATE Products
                SET quantity = @{nameof(newQuantity)}
                WHERE name = @{nameof(productName)}
                """;

        await sqlConnection.OpenAsync();

        using (var sqlCommand = new SqlCommand(query, sqlConnection))
        {
            sqlCommand.Parameters.AddWithValue($"@{nameof(newQuantity)}", newQuantity);
            sqlCommand.Parameters.AddWithValue($"@{nameof(productName)}", productName);
            await sqlCommand.ExecuteNonQueryAsync();
        }

        await sqlConnection.CloseAsync();
    }

    public async Task DeleteProductAsync(string productName)
    {   
        var query = $"""
                DELETE FROM Products
                WHERE name = @{nameof(productName)}
                """;

        await sqlConnection.OpenAsync();

        using (var sqlCommand = new SqlCommand(query, sqlConnection))
        {
            sqlCommand.Parameters.AddWithValue($"@{nameof(productName)}", productName);
            await sqlCommand.ExecuteNonQueryAsync();
        }

        await sqlConnection.CloseAsync();
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        var query = "SELECT * FROM Products";
        var products = new List<Product>();
        await sqlConnection.OpenAsync();

        using (var sqlCommand = new SqlCommand(query, sqlConnection))
        {
            using (var productsDataReader = sqlCommand.ExecuteReader())
            {
                while (productsDataReader.Read())
                {
                    products.Add(GetProductFromString(productsDataReader));
                }
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