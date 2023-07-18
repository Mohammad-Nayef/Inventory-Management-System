public class Product
{
    private string name;
    private decimal price;
    private int quantity;

    public Product(string name, decimal price, int quantity) 
    {
        this.name = name;
        this.price = price;
        this.quantity = quantity;
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
                Console.WriteLine("Please try again and enter a non-negative integer.");
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
                Console.WriteLine("Please try again and enter a non-negative integer.");
            else
                quantity = value; 
        }
    }
}