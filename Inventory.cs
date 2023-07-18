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
}