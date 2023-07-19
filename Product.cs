public class Product
{
    private string name;
    private decimal price;
    private int quantity; 

    public Product(string name, decimal price, int quantity) 
    {
        this.name = name;
        Price = price;
        Quantity = quantity;
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public decimal Price
    {
        get { return price; }
        set 
        {
            if (value < 0)
                Console.WriteLine("Please try again and enter a positive price.");
            else 
                price = value;
        }
    }

    public int Quantity
    {
        get { return quantity; }
        set 
        {
            if (value < 0)
                Console.WriteLine("Please try again and enter a positive quantity.");
            else
                quantity = value; 
        }
    }

    public static bool AreValid(decimal price, int quantity)
    {
        return price >= 0 && quantity >= 0;
    }
}