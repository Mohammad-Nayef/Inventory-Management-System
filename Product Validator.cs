public static class ProductValidator
{
    public static bool IsValid(this Product product)
    {
        return product.Price >= 0 && product.Quantity >= 0;
    }

    public static bool IsValidPrice(decimal productPrice)
    {
        return productPrice >= 0;
    }

    public static bool IsValidQuantity(int productQuantity)
    {
        return productQuantity >= 0;
    }
}