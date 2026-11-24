namespace SrpLab.Appointments;

public sealed class SmsReminderFormatter
{
    public string Format(DateTimeOffset slot, string clinicPhone)
    {
        return $"Reminder: appointment {slot:MMM dd HH:mm}. Call {clinicPhone} to reschedule.";
    }
}
