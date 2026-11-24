namespace SrpLab.Billing;

public sealed class InvoiceNumberGenerator
{
    private int _sequence = 1000;

    public string Next(DateOnly periodStart)
    {
        var n = Interlocked.Increment(ref _sequence);
        return $"INV-{periodStart:yyyyMM}-{n:D5}";
    }
}
