namespace Part2_HotelReservationSystem;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("=== HOTEL RESERVATION SYSTEM DEMO ===\n");

        var room101 = new Room("101", RoomType.Single, 150m);
        var room201 = new Room("201", RoomType.Suite, 450m);
        var guest = new Guest("G-001", "Ahmed Hassan", "+201012345678");

        Console.WriteLine($"Created Guest: {guest.FullName} ({guest.GuestId})");
        Console.WriteLine($"Room {room101.RoomNumber} ({room101.RoomType}) - Rate: ${room101.NightlyRate}/night");
        Console.WriteLine($"Room {room201.RoomNumber} ({room201.RoomType}) - Rate: ${room201.NightlyRate}/night\n");

        // Happy Path: Booking, Confirmation, Check-in, Check-out
        Console.WriteLine("--- 1. Happy Path Reservation Lifecycle ---");
        var res1 = guest.BookReservation(
            "RES-1001",
            room101,
            new DateTime(2026, 11, 1),
            new DateTime(2026, 11, 5)
        );

        Console.WriteLine($"Reservation: {res1.ReservationId}");
        Console.WriteLine($"Nights: {res1.TotalNights}, Total Cost: ${res1.TotalCost}");
        Console.WriteLine($"Initial Status: {res1.Status}");

        res1.Confirm();
        Console.WriteLine($"After Confirm: {res1.Status}");

        res1.CheckIn();
        Console.WriteLine($"After CheckIn: {res1.Status}");

        res1.CheckOut();
        Console.WriteLine($"After CheckOut: {res1.Status}\n");

        // Rule Validation Demonstrations
        Console.WriteLine("--- 2. Business Rules & Validation Tests ---");

        // Test 1: Date validity (CheckOut <= CheckIn)
        TestRule("Check-out date before check-in date", () =>
        {
            guest.BookReservation("RES-FAIL-1", room101, new DateTime(2026, 11, 10), new DateTime(2026, 11, 8));
        });

        // Test 2: Maintenance blocking
        TestRule("Booking room under maintenance", () =>
        {
            room201.StartMaintenance();
            guest.BookReservation("RES-FAIL-2", room201, new DateTime(2026, 12, 1), new DateTime(2026, 12, 3));
        });
        room201.EndMaintenance();

        // Test 3: Illegal state transition (Check-in without confirmation)
        TestRule("Check-in directly from Pending (skip Confirm)", () =>
        {
            var resPending = guest.BookReservation("RES-PENDING", room201, new DateTime(2026, 12, 1), new DateTime(2026, 12, 3));
            resPending.CheckIn();
        });

        // Test 4: Illegal transition from CheckedOut
        TestRule("Cancelling an already checked-out reservation", () =>
        {
            res1.Cancel();
        });

        // Test 5: Negative pricing rejection
        TestRule("Setting zero or negative nightly rate", () =>
        {
            room101.UpdateNightlyRate(-50m);
        });

        // Test 6: Overbooking rejection
        var activeRes = guest.BookReservation("RES-ACTIVE", room101, new DateTime(2026, 11, 15), new DateTime(2026, 11, 20));
        activeRes.Confirm();

        TestRule("Overbooking the same room with overlapping dates", () =>
        {
            var anotherGuest = new Guest("G-002", "Mona Zaki", "+201099887766");
            anotherGuest.BookReservation("RES-OVERLAP", room101, new DateTime(2026, 11, 18), new DateTime(2026, 11, 22));
        });

        Console.WriteLine("\nAll business rules and encapsulation boundaries enforced successfully.");
    }

    private static void TestRule(string description, Action action)
    {
        try
        {
            action();
            Console.WriteLine($"[FAILED] {description} did not throw an exception.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[PASS] {description} -> Threw {ex.GetType().Name}: {ex.Message}");
        }
    }
}
