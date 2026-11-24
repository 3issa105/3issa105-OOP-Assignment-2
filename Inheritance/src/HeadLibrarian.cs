using System;

namespace LibrarySystem
{
    public class HeadLibrarian : Staff
    {
        public HeadLibrarian(
            string personId,
            string fullName,
            string phone,
            DateTime hireDate,
            decimal monthlySalary)
            : base(personId, fullName, phone, hireDate, monthlySalary, 400m)
        {
        }

        public void ChangeLateFee(LibraryItem item, decimal newBaseFee)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            item.UpdateBaseLateFee(newBaseFee);
        }

        public void WithdrawItem(LibraryItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            item.Withdraw();
        }

        public void RestoreItem(LibraryItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            item.Restore();
        }
    }
}
