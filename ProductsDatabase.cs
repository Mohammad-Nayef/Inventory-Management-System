using System.Data.SqlClient;

public class ProductsDatabase
{
    private static readonly Lazy<ProductsDatabase> _lazyInstance = new Lazy<ProductsDatabase>(() => new ProductsDatabase());
    private static SqlConnection sqlConnection;
    private string connectionString = "Data Source = MOHAMMAD; " +
        "Initial Catalog = Inventory; " +
        "Integrated Security = True;";

    public static ProductsDatabase Instance => _lazyInstance.Value;

    private ProductsDatabase()
    {
        sqlConnection = new SqlConnection(connectionString);
        sqlConnection.Open();
        CreateProductsTable();
    }

    private void CreateProductsTable()
    {
        var query = """
                IF  object_id('Products') IS NULL
                BEGIN
                    create table products (
                	    name VARCHAR,
                        price FLOAT,
                        quantity INT
                    )
                END
                """;

        ExecuteQuery(query);
    }

    public void AddProduct(Product newProduct)
    {
        var query = $"""
                INSERT INTO Products
                VALUES ({newProduct.Name}, {newProduct.Price}, {newProduct.Quantity})
                """;

        ExecuteQuery(query);
    }

    public void ExecuteQuery(string query)
    {
        var sqlCommand = new SqlCommand(query, sqlConnection);
        sqlCommand.ExecuteNonQuery();
    }
}