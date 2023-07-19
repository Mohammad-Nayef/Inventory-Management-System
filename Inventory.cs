public class Inventory
{
    private List<Product> products = new();

    public void Add(string name, decimal price, int quantity)
    {
        Product newProduct = new(name, price, quantity);
        if (Product.AreValid(price, quantity)) 
        {
            products.Add(newProduct);
        }
    }

    public void Print()
    {
        for (int i = 0; i < products.Count; i++)
        {
            Console.Write($"Product #{i + 1}:\n");
            Console.WriteLine($"\tName: {products[i].Name}");
            Console.WriteLine($"\tPrice: {products[i].Price}");
            Console.WriteLine($"\tQuantity: {products[i].Quantity}\n");
        }
    }
}