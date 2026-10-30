namespace Part1_ProceduralToOOP;

public class Order
{
    private readonly List<OrderLine> _lines = new();

    public int Id { get; }
    public Customer Customer { get; }
    public string Date { get; }
    public bool IsPaid { get; private set; }
    public IReadOnlyList<OrderLine> Lines => _lines;

    public Order(int id, Customer customer, string date)
    {
        Id = id;
        Customer = customer;
        Date = date;
        IsPaid = false;
    }

    public bool TryAddLine(Product product, int quantity, out string errorMessage)
    {
        if (IsPaid)
        {
            errorMessage = "cannot change a paid order.";
            return false;
        }

        if (quantity <= 0)
        {
            errorMessage = "quantity must be positive.";
            return false;
        }

        if (!product.TryDeductStock(quantity))
        {
            errorMessage = $"not enough stock for product #{product.Id}.";
            return false;
        }

        _lines.Add(new OrderLine(product, quantity));
        errorMessage = string.Empty;
        return true;
    }

    public bool TryMarkPaid(out string errorMessage)
    {
        if (_lines.Count == 0)
        {
            errorMessage = "cannot pay an empty order.";
            return false;
        }

        IsPaid = true;
        errorMessage = string.Empty;
        return true;
    }

    public decimal CalculateTotal()
    {
        decimal total = _lines.Sum(line => line.LineTotal);

        if (Customer.IsVip)
            total *= 0.90m;

        return total;
    }
}
