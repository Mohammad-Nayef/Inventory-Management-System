Inventory inventory = new();
string name;
decimal price; 
int quantity, option;

while (true)
{
    Console.WriteLine("Inventory Management System\n");
    Console.WriteLine("1. Add a product.");
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
        default:
            Console.WriteLine("Please, enter a valid option.");
            break;
    }
    Console.Clear();
}