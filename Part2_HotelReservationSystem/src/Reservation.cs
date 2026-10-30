namespace Part2_HotelReservationSystem;

public class Reservation
{
    public string ReservationId { get; }
    public Guest Guest { get; }
    public Room Room { get; }
    public DateTime CheckInDate { get; }
    public DateTime CheckOutDate { get; }
    public ReservationStatus Status { get; private set; }

    public int TotalNights => (CheckOutDate.Date - CheckInDate.Date).Days;
    public decimal TotalCost => TotalNights * Room.NightlyRate;

    internal Reservation(string reservationId, Guest guest, Room room, DateTime checkInDate, DateTime checkOutDate)
    {
        if (string.IsNullOrWhiteSpace(reservationId))
            throw new ArgumentException("Reservation ID cannot be empty.", nameof(reservationId));

        ArgumentNullException.ThrowIfNull(guest);
        ArgumentNullException.ThrowIfNull(room);

        if (checkOutDate.Date <= checkInDate.Date)
            throw new ArgumentException("Check-out date must be strictly after check-in date.");

        if (room.IsUnderMaintenance)
            throw new InvalidOperationException($"Cannot create reservation. Room #{room.RoomNumber} is currently under maintenance.");

        if (room.HasOverlappingReservation(checkInDate, checkOutDate))
            throw new InvalidOperationException($"Room #{room.RoomNumber} already has an active reservation overlapping with requested dates.");

        ReservationId = reservationId;
        Guest = guest;
        Room = room;
        CheckInDate = checkInDate.Date;
        CheckOutDate = checkOutDate.Date;
        Status = ReservationStatus.Pending;
    }

    public void Confirm()
    {
        if (Status != ReservationStatus.Pending)
            throw new InvalidOperationException($"Cannot confirm reservation. Only Pending reservations can be confirmed. Current status: {Status}.");

        Status = ReservationStatus.Confirmed;
    }

    public void CheckIn()
    {
        if (Status != ReservationStatus.Confirmed)
            throw new InvalidOperationException($"Cannot check in reservation. Reservation must be Confirmed before checking in. Current status: {Status}.");

        Status = ReservationStatus.CheckedIn;
    }

    public void CheckOut()
    {
        if (Status != ReservationStatus.CheckedIn)
            throw new InvalidOperationException($"Cannot check out reservation. Reservation must be CheckedIn first. Current status: {Status}.");

        Status = ReservationStatus.CheckedOut;
    }

    public void Cancel()
    {
        if (Status != ReservationStatus.Pending && Status != ReservationStatus.Confirmed)
            throw new InvalidOperationException($"Cannot cancel reservation with status '{Status}'. Only Pending or Confirmed reservations can be cancelled.");

        Status = ReservationStatus.Cancelled;
    }
}
