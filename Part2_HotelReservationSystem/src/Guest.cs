namespace Part2_HotelReservationSystem;

public class Guest
{
    private readonly List<Reservation> _reservations = new();

    public string GuestId { get; }
    public string FullName { get; }
    public string PhoneNumber { get; }

    public IReadOnlyList<Reservation> Reservations => _reservations.AsReadOnly();

    public Guest(string guestId, string fullName, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(guestId))
            throw new ArgumentException("Guest ID cannot be empty.", nameof(guestId));

        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Guest full name cannot be empty.", nameof(fullName));

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Guest phone number cannot be empty.", nameof(phoneNumber));

        GuestId = guestId;
        FullName = fullName;
        PhoneNumber = phoneNumber;
    }

    public Reservation BookReservation(string reservationId, Room room, DateTime checkIn, DateTime checkOut)
    {
        var reservation = new Reservation(reservationId, this, room, checkIn, checkOut);
        _reservations.Add(reservation);
        room.AttachReservation(reservation);
        return reservation;
    }
}
