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
    }

    
}