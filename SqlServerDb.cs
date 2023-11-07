using System.Data.SqlClient;

public class SqlServerDb
{
    private static SqlConnection _sqlConnection;
    private string _connectionString;

    public SqlServerDb(string connectionString)
    {
        _connectionString = Constants.SqlServerConnectionString;
        _sqlConnection = new SqlConnection(_connectionString);
        InitializeDatabaseAsync().Wait();
    }

    private async Task InitializeDatabaseAsync()
    {
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

        await _sqlConnection.OpenAsync();

        using (var sqlCommand = new SqlCommand(query, _sqlConnection))
        {
            await sqlCommand.ExecuteNonQueryAsync();
        }

        await _sqlConnection.CloseAsync();
    }

    public async Task AddProductAsync(Product newProduct)
    {
        var query = $"""
                INSERT INTO Products
                VALUES (@{nameof(newProduct.Name)}, @{nameof(newProduct.Price)}, 
                @{nameof(newProduct.Quantity)})
                """;

        await _sqlConnection.OpenAsync();

        using (var sqlCommand = new SqlCommand(query, _sqlConnection))
        {
            sqlCommand.Parameters.AddWithValue($"@{nameof(newProduct.Name)}", newProduct.Name);
            sqlCommand.Parameters.AddWithValue($"@{nameof(newProduct.Price)}", newProduct.Price);
            sqlCommand.Parameters.AddWithValue($"@{nameof(newProduct.Quantity)}", newProduct.Quantity);
            await sqlCommand.ExecuteNonQueryAsync();
        }

        await _sqlConnection.CloseAsync();
    }

    public async Task EditProductNameAsync(string oldName, string newName)
    {
        var query = $"""
                UPDATE Products
                SET name = @{nameof(newName)}
                WHERE name = @{nameof(oldName)}
                """;

        await _sqlConnection.OpenAsync();

        using (var sqlCommand = new SqlCommand(query, _sqlConnection))
        {
            sqlCommand.Parameters.AddWithValue($"@{nameof(newName)}", newName);
            sqlCommand.Parameters.AddWithValue($"@{nameof(oldName)}", oldName);
            await sqlCommand.ExecuteNonQueryAsync();
        }

        await _sqlConnection.CloseAsync();
    }

    public async Task EditProductPriceAsync(string productName, decimal newPrice)
    {
        var query = $"""
                UPDATE Products
                SET price = @{nameof(newPrice)}
                WHERE name = @{nameof(productName)}
                """;

        await _sqlConnection.OpenAsync();

        using (var sqlCommand = new SqlCommand(query, _sqlConnection))
        {
            sqlCommand.Parameters.AddWithValue($"@{nameof(newPrice)}", newPrice);
            sqlCommand.Parameters.AddWithValue($"@{nameof(productName)}", productName);
            await sqlCommand.ExecuteNonQueryAsync();
        }

        await _sqlConnection.CloseAsync();
    }

    public async Task EditProductQuantityAsync(string productName, int newQuantity)
    {
        var query = $"""
                UPDATE Products
                SET quantity = @{nameof(newQuantity)}
                WHERE name = @{nameof(productName)}
                """;

        await _sqlConnection.OpenAsync();

        using (var sqlCommand = new SqlCommand(query, _sqlConnection))
        {
            sqlCommand.Parameters.AddWithValue($"@{nameof(newQuantity)}", newQuantity);
            sqlCommand.Parameters.AddWithValue($"@{nameof(productName)}", productName);
            await sqlCommand.ExecuteNonQueryAsync();
        }

        await _sqlConnection.CloseAsync();
    }

    public async Task DeleteProductAsync(string productName)
    {   
        var query = $"""
                DELETE FROM Products
                WHERE name = @{nameof(productName)}
                """;

        await _sqlConnection.OpenAsync();

        using (var sqlCommand = new SqlCommand(query, _sqlConnection))
        {
            sqlCommand.Parameters.AddWithValue($"@{nameof(productName)}", productName);
            await sqlCommand.ExecuteNonQueryAsync();
        }

        await _sqlConnection.CloseAsync();
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        var query = "SELECT * FROM Products";
        var products = new List<Product>();
        await _sqlConnection.OpenAsync();

        using (var sqlCommand = new SqlCommand(query, _sqlConnection))
        {
            using (var productsDataReader = sqlCommand.ExecuteReader())
            {
                while (productsDataReader.Read())
                {
                    products.Add(GetProductFromString(productsDataReader));
                }
            }
        }

        await _sqlConnection.CloseAsync();
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