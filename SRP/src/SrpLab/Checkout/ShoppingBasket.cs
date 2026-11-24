namespace SrpLab.Checkout;

public sealed class ShoppingBasket
{
    private readonly List<BasketItem> _items = new();

    public IReadOnlyList<BasketItem> Items => _items.AsReadOnly();
    public bool HasGiftWrap { get; private set; }

    public void AddLine(string sku, decimal price, int qty)
    {
        _items.Add(new BasketItem(sku, price, qty));
    }

    public void EnableGiftWrap()
    {
        HasGiftWrap = true;
    }

    public decimal SubTotal() => _items.Sum(item => item.LineTotal);
}
