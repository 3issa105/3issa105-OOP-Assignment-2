namespace SrpLab.Appointments;

public sealed class AppointmentScheduler
{
    private readonly ClinicHoursPolicy _hoursPolicy;
    private readonly HashSet<DateTimeOffset> _booked = new();

    public AppointmentScheduler(ClinicHoursPolicy hoursPolicy)
    {
        _hoursPolicy = hoursPolicy;
    }

    public DateTimeOffset? FindNextSlot(DateTimeOffset from, int searchHours)
    {
        var cursor = Align(from);
        var end = from.AddHours(searchHours);
        while (cursor < end)
        {
            if (_hoursPolicy.IsWithinBusinessHours(cursor) && !_booked.Contains(cursor))
                return cursor;

            cursor = cursor.AddMinutes(_hoursPolicy.SlotMinutes);
        }
        return null;
    }

    public bool TryBook(DateTimeOffset slot)
    {
        if (!_hoursPolicy.IsWithinBusinessHours(slot) || _booked.Contains(slot))
            return false;

        _booked.Add(slot);
        return true;
    }

    private DateTimeOffset Align(DateTimeOffset from)
    {
        var minutes = from.Minute - (from.Minute % _hoursPolicy.SlotMinutes);
        return new DateTimeOffset(from.Year, from.Month, from.Day, from.Hour, minutes, 0, from.Offset);
    }
}
