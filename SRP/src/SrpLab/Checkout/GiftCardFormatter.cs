namespace SrpLab.Checkout;

public sealed class GiftCardFormatter
{
    public string Format(ShoppingBasket basket, string fromName, decimal grandTotal)
    {
        var items = string.Join(", ", basket.Items.Select(i => i.Sku));
        return $"Dear friend,\nA gift from {fromName} awaits ({items}).\nTotal surprise value: {grandTotal:C}\n";
    }
}
