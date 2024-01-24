using System.Data.SqlClient;

public class SqlServerDatabaseInitializer
{
    protected string _connectionString = Constants.SqlServerConnectionString;

    public SqlServerDatabaseInitializer()
    {
        CreateProductsTableAsync().Wait();
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

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using var sqlCommand = new SqlCommand(query, connection);
            await sqlCommand.ExecuteNonQueryAsync();
        }
    }
}