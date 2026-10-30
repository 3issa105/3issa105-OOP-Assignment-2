namespace Part2_HotelReservationSystem;

public class Room
{
    private readonly List<Reservation> _reservations = new();

    public string RoomNumber { get; }
    public RoomType RoomType { get; }
    public decimal NightlyRate { get; private set; }
    public bool IsUnderMaintenance { get; private set; }

    public IReadOnlyList<Reservation> Reservations => _reservations.AsReadOnly();

    public Room(string roomNumber, RoomType roomType, decimal nightlyRate)
    {
        if (string.IsNullOrWhiteSpace(roomNumber))
            throw new ArgumentException("Room number cannot be empty.", nameof(roomNumber));

        if (nightlyRate <= 0)
            throw new ArgumentException("Nightly rate must be strictly positive.", nameof(nightlyRate));

        RoomNumber = roomNumber;
        RoomType = roomType;
        NightlyRate = nightlyRate;
        IsUnderMaintenance = false;
    }

    public void UpdateNightlyRate(decimal newRate)
    {
        if (newRate <= 0)
            throw new ArgumentException("Nightly rate must be strictly positive.", nameof(newRate));

        NightlyRate = newRate;
    }

    public void StartMaintenance()
    {
        IsUnderMaintenance = true;
    }

    public void EndMaintenance()
    {
        IsUnderMaintenance = false;
    }

    public bool HasOverlappingReservation(DateTime checkIn, DateTime checkOut)
    {
        return _reservations.Any(r =>
            r.Status != ReservationStatus.Cancelled &&
            r.Status != ReservationStatus.CheckedOut &&
            checkIn < r.CheckOutDate &&
            checkOut > r.CheckInDate);
    }

    internal void AttachReservation(Reservation reservation)
    {
        _reservations.Add(reservation);
    }
}
