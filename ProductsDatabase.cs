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

    public void EditProductName(string oldName, string newName)
    {
        var query = $"""
                UPDATE Products
                SET name = '{newName}'
                WHERE name = '{oldName}'
                """;

        ExecuteQuery(query);
    }

    public void EditProductPrice(string productName, decimal newPrice)
    {
        var query = $"""
                UPDATE Products
                SET price = '{newPrice}'
                WHERE name = '{productName}'
                """;

        ExecuteQuery(query);
    }

    public void EditProductQuantity(string productName, int newQuantity)
    {
        var query = $"""
                UPDATE Products
                SET quantity = '{newQuantity}'
                WHERE name = '{productName}'
                """;

        ExecuteQuery(query);
    }

    public void DeleteProduct(string productName)
    {
        var query = $"""
                DELETE FROM Products
                WHERE name = '{productName}'
                """;

        ExecuteQuery(query);
    }

    public void ExecuteQuery(string query)
    {
        var sqlCommand = new SqlCommand(query, sqlConnection);
        sqlCommand.ExecuteNonQuery();
    }

    public List<Product> GetAllProducts()
    {
        var query = """
            SELECT * FROM Products
            """;

        var sqlCommand = new SqlCommand(query, sqlConnection);
        var products = new List<Product>();

        using (var productsDataReader = sqlCommand.ExecuteReader())
        {
            while (productsDataReader.Read())
            {
                var name = productsDataReader.GetString(0);
                var price = (decimal)productsDataReader.GetDouble(1);
                var quantity = productsDataReader.GetInt32(2);

                products.Add(new Product(name, price, quantity));
            }
        }

        return products;
    }
}