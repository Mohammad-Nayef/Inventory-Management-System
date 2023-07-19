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

        // Print all of the existing products
        else
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

    public void Edit(EditOptions editOption, int index, object newValue)
    {
        switch(editOption)
        {
            case EditOptions.Name:
                products[index].Name = (string)newValue;
                break;
            case EditOptions.Price:
                products[index].Price = (decimal)newValue; 
                break;
            case EditOptions.Quantity:
                products[index].Quantity = (int)newValue;
                break;
            default:
                throw new Exception("Invalid edit option.");
        }
    }

    public int IndexOf(string name)
    {
        for (int i = 0; i < products.Count; i++)
        {
            if (products[i].Name.ToLower() == name.ToLower())
                return i;
        }
        return -1;
    }
}