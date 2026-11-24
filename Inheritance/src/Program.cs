using System;
using System.Collections.Generic;

namespace LibrarySystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== 1. Compile-time protection checks (commented out) ===");
            // Person p = new Person("P-0", "Test", "01000000000");               // must NOT compile
            // Member m = new Member("M-0", "Test", "01000000000", 3, 0m);         // must NOT compile
            // Staff s = new Staff("S-0", "Test", "01000000000", DateTime.Today, 5000m, 0m); // must NOT compile
            // LibraryItem item = new LibraryItem("C-0", "Test", 5m, 7, 1m);       // must NOT compile

            StudentMember student = new StudentMember("M-101", "Omar Hassan", "01012345678");
            PremiumMember premium = new PremiumMember("M-102", "Nora Adel", "01098765432", 20m);

            // student.FullName = "Changed Name";                                  // must NOT compile
            // student.Loans.Add(null);                                            // must NOT compile

            Book book1 = new Book("BK-01", "Clean Code", 10m);
            Book book2 = new Book("BK-02", "The Pragmatic Programmer", 10m);
            Book book3 = new Book("BK-03", "Design Patterns", 10m);
            Book book4 = new Book("BK-04", "Refactoring", 10m);
            DVD dvd1 = new DVD("DV-01", "Interstellar", 10m);
            Magazine mag1 = new Magazine("MG-01", "National Geographic", 10m);

            // book1.IsOnLoan = true;                                              // must NOT compile

            Console.WriteLine("Protected constructors, read-only properties, and encapsulation verified.\n");

            Console.WriteLine("=== 2. Staff list & monthly pay ===");
            Librarian librarian = new Librarian("ST-01", "Mona Ali", "01111111111", new DateTime(2022, 3, 1), 6000m);
            HeadLibrarian headLibrarian = new HeadLibrarian("ST-02", "Karim Tarek", "01222222222", new DateTime(2020, 1, 15), 9000m);
            Shelver shelver = new Shelver("ST-03", "Youssef Nabil", "01555555555", new DateTime(2024, 6, 10), 4000m, "Fiction");

            List<Staff> staffList = new List<Staff>();
            staffList.Add(librarian);
            staffList.Add(headLibrarian);
            staffList.Add(shelver);

            foreach (Staff member in staffList)
            {
                Console.WriteLine(member.FullName + " | Monthly Pay: " + member.GetMonthlyPay());
            }

            librarian.GiveRaise(10m);
            shelver.Reassign("Children");
            Console.WriteLine("After raise: " + librarian.FullName + " new pay = " + librarian.GetMonthlyPay());
            Console.WriteLine("After reassign: " + shelver.FullName + " section = " + shelver.Section + "\n");

            Console.WriteLine("=== 3. Library items & daily late fees ===");
            List<LibraryItem> catalog = new List<LibraryItem>();
            catalog.Add(book1);
            catalog.Add(dvd1);
            catalog.Add(mag1);

            foreach (LibraryItem catalogItem in catalog)
            {
                Console.WriteLine(
                    catalogItem.Title +
                    " | Period: " + catalogItem.LoanPeriodDays + " days" +
                    " | Daily Late Fee: " + catalogItem.GetDailyLateFee());
            }

            Console.WriteLine();

            Console.WriteLine("=== 4. Borrowing rules & rejections ===");
            headLibrarian.WithdrawItem(book4);
            try
            {
                student.BorrowItem("LN-001", book4, new DateTime(2026, 10, 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Withdrawn item rejected: " + ex.Message);
            }
            headLibrarian.RestoreItem(book4);

            Loan studentLoan1 = student.BorrowItem("LN-002", book1, new DateTime(2026, 10, 1));
            try
            {
                premium.BorrowItem("LN-003", book1, new DateTime(2026, 10, 2));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Double loan rejected: " + ex.Message);
            }

            student.BorrowItem("LN-004", book2, new DateTime(2026, 10, 1));
            student.BorrowItem("LN-005", book3, new DateTime(2026, 10, 1));
            try
            {
                student.BorrowItem("LN-006", book4, new DateTime(2026, 10, 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loan limit rejected: " + ex.Message);
            }

            Console.WriteLine();

            Console.WriteLine("=== 5. Premium member DVD late return & reading points ===");
            DateTime dvdBorrowDate = new DateTime(2026, 10, 1);
            Loan dvdLoan = premium.BorrowItem("LN-007", dvd1, dvdBorrowDate);
            DateTime dvdReturnDate = dvdLoan.DueDate.AddDays(5);
            librarian.ProcessReturn(dvdLoan, dvdReturnDate);

            Console.WriteLine("DVD Due Date: " + dvdLoan.DueDate.ToString("yyyy-MM-dd"));
            if (dvdLoan.ReturnDate.HasValue)
            {
                Console.WriteLine("Returned On: " + dvdLoan.ReturnDate.Value.ToString("yyyy-MM-dd") + " (5 days late)");
            }
            Console.WriteLine("Late Fee (20% discount applied): " + dvdLoan.LateFee);
            Console.WriteLine("Premium Member Reading Points: " + premium.ReadingPoints + "\n");

            Console.WriteLine("=== 6. Illegal status transitions ===");
            try
            {
                librarian.ProcessReturn(dvdLoan, dvdReturnDate.AddDays(1));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Second return rejected: " + ex.Message);
            }

            try
            {
                librarian.MarkLoanAsLost(dvdLoan);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Marking returned loan as lost rejected: " + ex.Message);
            }

            Console.WriteLine();

            Console.WriteLine("=== 7. Input validation checks ===");
            try
            {
                librarian.GiveRaise(0m);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Zero raise rejected: " + ex.Message);
            }

            try
            {
                headLibrarian.ChangeLateFee(mag1, -5m);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Negative fee rejected: " + ex.Message);
            }

            try
            {
                new StudentMember("", "Invalid", "0100");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Empty ID rejected: " + ex.Message);
            }

            try
            {
                librarian.ProcessReturn(studentLoan1, studentLoan1.BorrowDate.AddDays(-1));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid return date rejected: " + ex.Message);
            }
        }
    }
}
