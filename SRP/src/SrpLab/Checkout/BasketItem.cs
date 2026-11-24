namespace SrpLab.Checkout;

public sealed class BasketItem
{
    public string Sku { get; }
    public decimal UnitPrice { get; }
    public int Quantity { get; }
    public decimal LineTotal => UnitPrice * Quantity;

    public BasketItem(string sku, decimal unitPrice, int quantity)
    {
        if (string.IsNullOrWhiteSpace(sku)) throw new ArgumentException("sku required", nameof(sku));
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));

        Sku = sku;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }
}
