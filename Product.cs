public class Product
{
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public Product(string? name, decimal price, int quantity) 
    {
        (Name, Price, Quantity) = (name, price, quantity);
    }

    public override string ToString()
    {
        return $"""
            Name: {Name}
            Price: {Price}
            Quantity: {Quantity}
            """;
    }

    public static bool ValidPrice(decimal productPrice)
    {
        return productPrice >= 0;
    }
    
    public static bool ValidQuantity(int productQuantity)
    {
        return productQuantity >= 0;
    }
}