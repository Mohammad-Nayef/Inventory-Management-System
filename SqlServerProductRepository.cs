using System.Data.SqlClient;

public class SqlServerProductRepository : SqlServerDatabaseInitializer, IProductRepository
{
    public async Task AddProductAsync(Product newProduct)
    {
        var query = $"""
                INSERT INTO Products
                VALUES (@{nameof(newProduct.Name)}, @{nameof(newProduct.Price)}, 
                @{nameof(newProduct.Quantity)})
                """;

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using var sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.AddWithValue($"@{nameof(newProduct.Name)}", newProduct.Name);
            sqlCommand.Parameters.AddWithValue($"@{nameof(newProduct.Price)}", newProduct.Price);
            sqlCommand.Parameters.AddWithValue($"@{nameof(newProduct.Quantity)}", newProduct.Quantity);
            await sqlCommand.ExecuteNonQueryAsync();
        }
    }

    public async Task EditProductNameAsync(string oldName, string newName)
    {
        var query = $"""
                UPDATE Products
                SET name = @{nameof(newName)}
                WHERE name = @{nameof(oldName)}
                """;

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using var sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.AddWithValue($"@{nameof(newName)}", newName);
            sqlCommand.Parameters.AddWithValue($"@{nameof(oldName)}", oldName);
            await sqlCommand.ExecuteNonQueryAsync();
        }
    }

    public async Task EditProductPriceAsync(string productName, decimal newPrice)
    {
        var query = $"""
                UPDATE Products
                SET price = @{nameof(newPrice)}
                WHERE name = @{nameof(productName)}
                """;

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using var sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.AddWithValue($"@{nameof(newPrice)}", newPrice);
            sqlCommand.Parameters.AddWithValue($"@{nameof(productName)}", productName);
            await sqlCommand.ExecuteNonQueryAsync();
        }
    }

    public async Task EditProductQuantityAsync(string productName, int newQuantity)
    {
        var query = $"""
                UPDATE Products
                SET quantity = @{nameof(newQuantity)}
                WHERE name = @{nameof(productName)}
                """;

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using var sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.AddWithValue($"@{nameof(newQuantity)}", newQuantity);
            sqlCommand.Parameters.AddWithValue($"@{nameof(productName)}", productName);
            await sqlCommand.ExecuteNonQueryAsync();
        }
    }

    public async Task DeleteProductAsync(string productName)
    {
        var query = $"""
                DELETE FROM Products
                WHERE name = @{nameof(productName)}
                """;

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using var sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.AddWithValue($"@{nameof(productName)}", productName);
            await sqlCommand.ExecuteNonQueryAsync();
        }
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        var query = "SELECT * FROM Products";
        var products = new List<Product>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using var sqlCommand = new SqlCommand(query, connection);
            using var productsDataReader = sqlCommand.ExecuteReader();

            while (productsDataReader.Read())
            {
                products.Add(GetProductFromString(productsDataReader));
            }
        }

        return products;
    }

    private static Product GetProductFromString(SqlDataReader productsDataReader)
    {
        var name = productsDataReader.GetString(0);
        var price = (decimal)productsDataReader.GetDouble(1);
        var quantity = productsDataReader.GetInt32(2);

        return new Product(name, price, quantity);
    }

    public async Task<bool> IsEmptyAsync()
    {
        var query = "SELECT TOP 1 * FROM Products ";

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using var sqlCommand = new SqlCommand(query, connection);
            using var productsDataReader = sqlCommand.ExecuteReader();

            if (productsDataReader.HasRows)
            {
                return false;
            }

            return true;
        }
    }
}
