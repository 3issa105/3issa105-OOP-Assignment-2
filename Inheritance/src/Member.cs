using System;
using System.Collections.Generic;

namespace LibrarySystem
{
    public class Member : Person
    {
        private readonly List<Loan> _loans;

        public int MaxLoans { get; }
        public decimal LateFeeDiscountPercent { get; }

        public IReadOnlyList<Loan> Loans
        {
            get { return _loans.AsReadOnly(); }
        }

        protected Member(
            string personId,
            string fullName,
            string phone,
            int maxLoans,
            decimal lateFeeDiscountPercent)
            : base(personId, fullName, phone)
        {
            if (maxLoans <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxLoans), "Max loans must be positive.");
            }

            if (lateFeeDiscountPercent < 0m || lateFeeDiscountPercent > 100m)
            {
                throw new ArgumentOutOfRangeException(nameof(lateFeeDiscountPercent), "Discount percent must be between 0 and 100.");
            }

            MaxLoans = maxLoans;
            LateFeeDiscountPercent = lateFeeDiscountPercent;
            _loans = new List<Loan>();
        }

        public int GetActiveLoanCount()
        {
            int count = 0;
            foreach (Loan loan in _loans)
            {
                if (loan.Status == LoanStatus.Borrowed)
                {
                    count++;
                }
            }
            return count;
        }

        public Loan BorrowItem(string loanId, LibraryItem item, DateTime borrowDate)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (GetActiveLoanCount() >= MaxLoans)
            {
                throw new InvalidOperationException(FullName + " has reached the maximum loan limit of " + MaxLoans + ".");
            }

            Loan loan = new Loan(loanId, this, item, borrowDate);
            _loans.Add(loan);
            return loan;
        }
    }
}
