namespace Part1_ProceduralToOOP;

public class Product
{
    public int Id { get; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; private set; }

    public Product(int id, string name, decimal price, int stock)
    {
        Id = id;
        Name = name;
        Price = price;
        Stock = stock;
    }

    public bool HasSufficientStock(int quantity)
    {
        return quantity > 0 && Stock >= quantity;
    }

    public bool TryDeductStock(int quantity)
    {
        if (!HasSufficientStock(quantity))
            return false;

        Stock -= quantity;
        return true;
    }
}
