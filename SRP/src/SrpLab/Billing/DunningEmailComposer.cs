namespace SrpLab.Billing;

public sealed class DunningEmailComposer
{
    public string Compose(Subscription sub, string customerName, DateOnly asOf, decimal amountDue, string invoiceNumber)
    {
        var severity = sub.FailedPayments switch
        {
            <= 1 => "friendly reminder",
            2 => "second notice",
            _ => "final notice before suspension"
        };

        return $"Subject: {severity} {invoiceNumber}\nHi {customerName},\nBalance {amountDue:C} as of {asOf:o} ({sub.FailedPayments} failures).\n";
    }
}
