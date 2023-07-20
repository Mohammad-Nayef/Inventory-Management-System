Inventory inventory = new();
string name;
decimal price; 
int quantity, option;

while (true)
{
    Console.WriteLine("Inventory Management System\n");
    Console.WriteLine("1. Add a product.");
    Console.WriteLine("2. View all products.");
    Console.WriteLine("3. Edit a product.");
    Console.WriteLine("4. Delete a product.");
    Console.WriteLine("5. Search for a product.");
    Console.WriteLine("0. Exit.\n");
    Console.Write("Choose a valid option: ");
    option = int.Parse(Console.ReadLine());

    switch (option)
    {
        case 0:
            return 0;

        case 1:
            Console.Write("Name of the product: ");
            name = Console.ReadLine();
            Console.Write("Price of the product: ");
            price = decimal.Parse(Console.ReadLine());
            Console.Write("Quantity of the product: ");
            quantity = int.Parse(Console.ReadLine());
            inventory.Add(name, price, quantity);
            break;

        case 2:
            inventory.Print();
            break;

        case 3:
            Console.Write("Name of a product to edit it: ");
            name = Console.ReadLine();
            Edit(name);
            break;

        case 4:
            Console.Write("Name of a product to delete it: ");
            name = Console.ReadLine();
            inventory.Delete(name);
            break;

        case 5:
            Console.Write("Name of a product to search for it: ");
            name = Console.ReadLine();
            int index = inventory.IndexOf(name);
            if (index == -1)
                Console.WriteLine($"{name} doesn't exist.");

            else
            {
                Console.WriteLine();
                inventory.Print(index);
            }
            break;

        default:
            Console.WriteLine("Please enter a valid option.");
            break;
    }

    Console.Write("\nPress any key to continue.");
    Console.ReadKey();
    Console.Clear();
}

void Edit(string name)
{
    int index = inventory.IndexOf(name);
    if (index == -1)
    {
        Console.WriteLine($"{name} doesn't exist.");
        return;
    }

    Console.Clear();
    Console.WriteLine($"The current data of {name}: ");
    inventory.Print(index);

    Console.WriteLine("\n1. Edit the name.");
    Console.WriteLine("2. Edit the price.");
    Console.WriteLine("3. Edit the quantity.");
    Console.WriteLine("0. Cancel.\n");
    Console.Write("Choose a valid option: ");

    int option;
    option = int.Parse(Console.ReadLine());
    switch (option)
    {
        case 0:
            return;

        case 1:
            Console.Write("Enter the new name: ");
            string newName = Console.ReadLine();
            inventory.Edit(EditOptions.Name, index, newName);
            break;

        case 2:
            Console.Write("Enter the new price: ");
            decimal newPrice = decimal.Parse(Console.ReadLine());
            inventory.Edit(EditOptions.Price, index, newPrice);
            break;

        case 3:
            Console.Write("Enter the new quantity: ");
            int newQuantity = int.Parse(Console.ReadLine());
            inventory.Edit(EditOptions.Quantity, index, newQuantity);
            break;

        default:
            Console.WriteLine("Please use a valid edit option.");
            break;
    }
}