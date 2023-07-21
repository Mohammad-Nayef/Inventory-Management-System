public class Product
{
    private decimal _price;
    private int _quantity;
    public string Name { get; set; }

    public Product(string name, decimal price, int quantity) 
    {
        (Name, Price, Quantity) = (name, price, quantity);
    }

    public decimal Price
    {
        get { return _price; }
        set 
        {
            if (value < 0)
                Console.WriteLine("Please try again and enter a positive price.");
            else 
                _price = value;
        }
    }

    public int Quantity
    {
        get { return _quantity; }
        set 
        {
            if (value < 0)
                Console.WriteLine("Please try again and enter a positive quantity.");
            else
                _quantity = value; 
        }
    }

    public static bool AreValid(decimal price, int quantity)
    {
        return price >= 0 && quantity >= 0;
    }
}