using System;

namespace LibrarySystem
{
    public class Loan
    {
        public string LoanId { get; }
        public Member Member { get; }
        public LibraryItem Item { get; }
        public DateTime BorrowDate { get; }
        public DateTime? ReturnDate { get; private set; }
        public LoanStatus Status { get; private set; }

        public DateTime DueDate
        {
            get { return BorrowDate.AddDays(Item.LoanPeriodDays); }
        }

        public decimal LateFee
        {
            get
            {
                if (Status != LoanStatus.Returned || !ReturnDate.HasValue)
                {
                    return 0m;
                }

                int lateDays = (ReturnDate.Value.Date - DueDate.Date).Days;
                if (lateDays <= 0)
                {
                    return 0m;
                }

                decimal grossFee = lateDays * Item.GetDailyLateFee();
                decimal discount = grossFee * (Member.LateFeeDiscountPercent / 100m);
                return Math.Round(grossFee - discount, 2);
            }
        }

        internal Loan(string loanId, Member member, LibraryItem item, DateTime borrowDate)
        {
            if (string.IsNullOrWhiteSpace(loanId))
            {
                throw new ArgumentException("Loan ID cannot be empty.", nameof(loanId));
            }

            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            item.MarkAsBorrowed();

            LoanId = loanId.Trim();
            Member = member;
            Item = item;
            BorrowDate = borrowDate;
            ReturnDate = null;
            Status = LoanStatus.Borrowed;
        }

        internal void MarkReturned(DateTime returnDate)
        {
            if (Status != LoanStatus.Borrowed)
            {
                throw new InvalidOperationException("Loan " + LoanId + " cannot be returned because its status is " + Status + ".");
            }

            if (returnDate < BorrowDate)
            {
                throw new ArgumentException("Return date cannot be earlier than borrow date.", nameof(returnDate));
            }

            ReturnDate = returnDate;
            Status = LoanStatus.Returned;
            Item.MarkAsReturned();
        }

        internal void MarkLost()
        {
            if (Status != LoanStatus.Borrowed)
            {
                throw new InvalidOperationException("Loan " + LoanId + " cannot be marked as lost because its status is " + Status + ".");
            }

            Status = LoanStatus.Lost;
        }
    }
}
