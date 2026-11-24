namespace SrpLab.Support;

public sealed class PublicReplyComposer
{
    public string Draft(SupportTicket ticket, string agentName, DateTimeOffset slaDeadline)
    {
        var apology = ticket.Priority == "P1"
            ? "We are treating this as a critical incident."
            : "Thanks for reaching out.";

        return $"Hi,\n{apology}\nTicket {ticket.Id} is with {agentName}. Next update before {slaDeadline:u}.\n";
    }
}
