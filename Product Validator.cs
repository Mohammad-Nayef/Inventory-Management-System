public static class ProductValidator
{
    public static bool IsValid(this Product product)
    {
        return product.Price >= 0 && product.Quantity >= 0;
    }
}