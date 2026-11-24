namespace SrpLab.Billing;

public sealed class LedgerJournalFormatter
{
    public string FormatLine(Subscription sub, string invoiceNumber, decimal amount)
    {
        return $"{sub.CustomerId},{invoiceNumber},{amount:0.00},AR-SUB";
    }
}
