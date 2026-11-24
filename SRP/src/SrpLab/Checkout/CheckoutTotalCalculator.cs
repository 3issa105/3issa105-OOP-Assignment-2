namespace SrpLab.Checkout;

public sealed class CheckoutTotalCalculator
{
    private const decimal GiftWrapFee = 4.99m;

    public decimal CalculateGrandTotal(ShoppingBasket basket, decimal discount)
    {
        var total = basket.SubTotal() - discount;
        if (basket.HasGiftWrap)
            total += GiftWrapFee;

        return Math.Max(0m, total);
    }
}
