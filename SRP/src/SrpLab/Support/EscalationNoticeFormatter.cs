namespace SrpLab.Support;

public sealed class EscalationNoticeFormatter
{
    public string Format(SupportTicket ticket, DateTimeOffset slaDeadline)
    {
        return $"ESCALATE {ticket.Id} priority={ticket.Priority} breachAt={slaDeadline:u} keywords-scanned=yes";
    }
}
