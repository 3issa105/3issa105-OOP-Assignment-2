using System;

namespace LibrarySystem
{
    public class LibraryItem
    {
        private readonly decimal _lateFeeMultiplier;

        public string CatalogNumber { get; }
        public string Title { get; }
        public int LoanPeriodDays { get; }
        public decimal BaseLateFee { get; private set; }
        public bool IsWithdrawn { get; private set; }
        public bool IsOnLoan { get; private set; }

        protected LibraryItem(
            string catalogNumber,
            string title,
            decimal baseLateFee,
            int loanPeriodDays,
            decimal lateFeeMultiplier)
        {
            if (string.IsNullOrWhiteSpace(catalogNumber))
            {
                throw new ArgumentException("Catalog number cannot be empty.", nameof(catalogNumber));
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be empty.", nameof(title));
            }

            if (baseLateFee <= 0m)
            {
                throw new ArgumentOutOfRangeException(nameof(baseLateFee), "Base late fee must be greater than zero.");
            }

            if (loanPeriodDays <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(loanPeriodDays), "Loan period must be positive.");
            }

            if (lateFeeMultiplier <= 0m)
            {
                throw new ArgumentOutOfRangeException(nameof(lateFeeMultiplier), "Multiplier must be positive.");
            }

            CatalogNumber = catalogNumber.Trim();
            Title = title.Trim();
            BaseLateFee = baseLateFee;
            LoanPeriodDays = loanPeriodDays;
            _lateFeeMultiplier = lateFeeMultiplier;
            IsWithdrawn = false;
            IsOnLoan = false;
        }

        public decimal GetDailyLateFee()
        {
            return BaseLateFee * _lateFeeMultiplier;
        }

        internal void UpdateBaseLateFee(decimal newBaseFee)
        {
            if (newBaseFee <= 0m)
            {
                throw new ArgumentOutOfRangeException(nameof(newBaseFee), "Late fee must be greater than zero.");
            }

            BaseLateFee = newBaseFee;
        }

        internal void Withdraw()
        {
            IsWithdrawn = true;
        }

        internal void Restore()
        {
            IsWithdrawn = false;
        }

        internal void MarkAsBorrowed()
        {
            if (IsWithdrawn)
            {
                throw new InvalidOperationException("Cannot borrow withdrawn item: " + Title + ".");
            }

            if (IsOnLoan)
            {
                throw new InvalidOperationException("Item is already on loan: " + Title + ".");
            }

            IsOnLoan = true;
        }

        internal void MarkAsReturned()
        {
            IsOnLoan = false;
        }
    }
}
