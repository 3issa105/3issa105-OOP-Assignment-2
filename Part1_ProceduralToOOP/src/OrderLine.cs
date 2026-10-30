namespace Part1_ProceduralToOOP;

public class OrderLine
{
    public Product Product { get; }
    public int Quantity { get; }
    public decimal LineTotal => Product.Price * Quantity;

    public OrderLine(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }
}
