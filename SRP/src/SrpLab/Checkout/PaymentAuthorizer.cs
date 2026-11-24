namespace SrpLab.Checkout;

public sealed class PaymentAuthorizer
{
    public string Authorize(decimal grandTotal, string cardLast4, int itemCount)
    {
        var payload = $"{grandTotal:0.00}|{cardLast4}|{itemCount}";
        var hash = payload.GetHashCode();
        return $"AUTH-{Math.Abs(hash):X8}";
    }
}
