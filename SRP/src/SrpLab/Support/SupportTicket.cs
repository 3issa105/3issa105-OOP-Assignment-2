namespace SrpLab.Support;

public sealed class SupportTicket
{
    public string Id { get; }
    public string Subject { get; }
    public string Body { get; private set; }
    public DateTimeOffset OpenedAt { get; }
    public string Priority { get; private set; }

    public SupportTicket(string id, string subject, string body, DateTimeOffset openedAt, string priority = "P3")
    {
        Id = id;
        Subject = subject;
        Body = body;
        OpenedAt = openedAt;
        Priority = priority;
    }

    public void AppendCustomerMessage(string text)
    {
        Body += "\n---\n" + text;
    }

    public void UpdatePriority(string priority)
    {
        Priority = priority;
    }
}
