namespace SrpLab.Checkout;

public sealed class CouponDiscountCalculator
{
    public decimal CalculateDiscount(string? couponText, decimal subTotal)
    {
        if (string.IsNullOrWhiteSpace(couponText)) return 0m;

        var code = couponText.Trim().ToUpperInvariant();
        if (code.StartsWith("SAVE") && int.TryParse(code[4..], out var pct) && pct is > 0 and <= 50)
            return Math.Round(subTotal * pct / 100m, 2);

        if (code.Contains("FREESHIP")) return 0m;
        if (code == "WELCOME10") return Math.Min(10m, subTotal);

        return 0m;
    }
}
