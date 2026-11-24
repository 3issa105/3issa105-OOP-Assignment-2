namespace LibrarySystem
{
    public class PremiumMember : Member
    {
        public PremiumMember(string personId, string fullName, string phone, decimal lateFeeDiscountPercent)
            : base(personId, fullName, phone, 10, lateFeeDiscountPercent)
        {
        }

        public int ReadingPoints
        {
            get
            {
                int points = 0;
                foreach (Loan loan in Loans)
                {
                    if (loan.Status == LoanStatus.Returned)
                    {
                        points += 5;
                    }
                }
                return points;
            }
        }
    }
}
