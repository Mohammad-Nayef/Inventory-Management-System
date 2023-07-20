using System.ComponentModel;

public class Product
{
    private decimal price;
    private int quantity;
    public string Name { get; set; }

    public Product(string name, decimal price, int quantity) 
    {
        (Name, Price, Quantity) = (name, price, quantity);
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