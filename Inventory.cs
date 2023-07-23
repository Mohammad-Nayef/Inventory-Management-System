public class Inventory
{
    private List<Product> products = new();

    public void Add(string name, decimal price, int quantity)
    {
        var newProduct = new Product(name, price, quantity);
        if (Product.AreValid(price, quantity)) 
        {
            products.Add(newProduct);
        }
    }

    public bool IsEmpty()
    {
        return products.Count == 0;
    }

    public void Print(int index = -1)
    {
        if (IsEmpty())
            Console.WriteLine("There are no products.");

        // Print a specific product
        else if (index != -1)
        {
            Console.WriteLine($"Name: {products[index].Name}");
            Console.WriteLine($"Price: {products[index].Price}");
            Console.WriteLine($"Quantity: {products[index].Quantity}");
        }

        // Print all of the existing products (without passing an argument)
        else
        {
            for (var i = 0; i < products.Count; i++)
            {
                Console.Write($"Product #{i + 1}:\n");
                Console.WriteLine($"\tName: {products[i].Name}");
                Console.WriteLine($"\tPrice: {products[i].Price}");
                Console.WriteLine($"\tQuantity: {products[i].Quantity}\n");
            }
        }
    }

    public void Edit(int index, object newValue)
    {
        // The name is edited
        if (newValue.GetType() == typeof(string))
            products[index].Name = (string)newValue;

        // The price is edited
        else if (newValue.GetType() == typeof(decimal))
            products[index].Price = (decimal)newValue;
        
        // The quantity is edited
        else if (newValue.GetType() == typeof(int))
            products[index].Quantity = (int)newValue;

        else
            throw new Exception("The edited value has an invalid data type.");
    }

    public void Delete(string neededName)
    {
        var index = IndexOf(neededName);
        if (index == -1)
            Console.WriteLine($"{neededName} doesn't exist.");

        else
            products.RemoveAt(index);
    }

    public int IndexOf(string name)
    {
        for (var i = 0; i < products.Count; i++)
        {
            if (products[i].Name.ToLower() == name.ToLower())
                return i;
        }
        return -1;
    }
}