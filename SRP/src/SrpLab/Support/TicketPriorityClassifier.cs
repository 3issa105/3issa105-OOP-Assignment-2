namespace SrpLab.Support;

public sealed class TicketPriorityClassifier
{
    public string Classify(string subject, string body)
    {
        var blob = (subject + " " + body).ToLowerInvariant();
        if (blob.Contains("down") || blob.Contains("outage") || blob.Contains("cannot login"))
            return "P1";
        if (blob.Contains("urgent") || blob.Contains("asap") || blob.Contains("blocked"))
            return "P2";

        return "P3";
    }
}
