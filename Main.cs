var inventory = new Inventory(new MongoDbProductRepository(Constants.MongoDbConnectionString));
string? productName;
decimal productPrice; 
int productQuantity, menuOption;

while (true)
{
    PrintMainMenu();
    bool converted = int.TryParse(Console.ReadLine(), out menuOption);
    if (!converted)
    {
        Console.WriteLine("Invalid option.");
        continue;
    }

    switch (menuOption)
    {
        case 0:
            return 0;

        case 1:
            Console.Write("Name of the product: ");
            productName = Console.ReadLine();

            Console.Write("Price of the product: ");
            converted = decimal.TryParse(Console.ReadLine(), out productPrice);
            if (!converted)
            {
                Console.WriteLine("Invalid price.");
                break;
            }

            Console.Write("Quantity of the product: ");
            converted = int.TryParse(Console.ReadLine(), out productQuantity);
            if (!converted)
            {
                Console.WriteLine("Invalid quantity.");
                break;
            }

            var newProduct = new Product(productName, productPrice, productQuantity);

            if (newProduct.IsValid())
                await inventory.AddProductAsync(newProduct);
            else
                Console.WriteLine("The values can't be negative.");

            break;

        case 2:
            Console.WriteLine(await inventory.PrintAllProductsAsync());
            break;

        case 3:
            Console.Write("Name of a product to edit it: ");
            productName = Console.ReadLine();

            var product = await inventory.FindProductAsync(productName);
            if (product == null)
            {
                Console.WriteLine("The product is not found.");
                break;
            }
              
            await EditProductMenuAsync(product);
            break;

        case 4:
            Console.Write("Name of a product to delete it: ");
            productName = Console.ReadLine();

            product = await inventory.FindProductAsync(productName);
            if (product == null)
            {
                Console.WriteLine("The product is not found.");
                break;
            }

            await inventory.DeleteProductAsync(product);
            break;

        case 5:
            Console.Write("Name of a product to search for it: ");
            productName = Console.ReadLine();

            product = await inventory.FindProductAsync(productName);
            if (product == null)
            {
                Console.WriteLine("The product is not found.");
                break;
            }

            Console.WriteLine(product.ToString());
            break;

        default:
            Console.WriteLine("Please enter a valid option.");
            break;
    }

    Console.Write("\nPress any key to continue.");
    Console.ReadKey();
    Console.Clear();
}

async Task EditProductMenuAsync(Product product)
{
    Console.Clear();
    Console.WriteLine($"The current data of {product.Name}: ");
    Console.WriteLine(product.ToString());

    PrintEditMenu();
    bool converted = int.TryParse(Console.ReadLine(), out menuOption);
    if (!converted)
    {
        Console.WriteLine("Invalid option.");
        return;
    }

    switch (menuOption)
    {
        case 0:
            return;

        case 1:
            Console.Write("Enter the new name: ");
            var newName = Console.ReadLine();

            await inventory.EditProductNameAsync(product, newName);
            break;

        case 2:
            Console.Write("Enter the new price: ");
            converted = decimal.TryParse(Console.ReadLine(), out var newPrice);
            if (!converted || !ProductValidator.IsValidPrice(newPrice))
            {
                Console.WriteLine("Invalid price.");
                return;
            }

            await inventory.EditProductPriceAsync(product, newPrice);
            break;

        case 3:
            Console.Write("Enter the new quantity: ");
            converted = int.TryParse(Console.ReadLine(), out var newQuantity);
            if (!converted || !ProductValidator.IsValidQuantity(newQuantity))
            {
                Console.WriteLine("Invalid quantity.");
                break;
            }

            await inventory.EditProductQuantityAsync(product, newQuantity);
            break;

        default:
            Console.WriteLine("Please enter a valid edit option.");
            break;
    }
}

static void PrintMainMenu()
{
    Console.WriteLine("Inventory Management System\n");
    Console.WriteLine("1. Add a product.");
    Console.WriteLine("2. View all products.");
    Console.WriteLine("3. Edit a product.");
    Console.WriteLine("4. Delete a product.");
    Console.WriteLine("5. Search for a product.");
    Console.WriteLine("0. Exit.\n");
    Console.Write("Enter a valid option: ");
}

static void PrintEditMenu()
{
    Console.WriteLine("\n1. Edit the name.");
    Console.WriteLine("2. Edit the price.");
    Console.WriteLine("3. Edit the quantity.");
    Console.WriteLine("0. Cancel.\n");
    Console.Write("Enter a valid option: ");
}