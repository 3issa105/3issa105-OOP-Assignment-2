namespace SrpLab.Billing;

public sealed class ProrationCalculator
{
    public decimal Calculate(Subscription sub, DateOnly activeFrom)
    {
        if (activeFrom <= sub.PeriodStart) return sub.MonthlyPrice;
        if (activeFrom >= sub.PeriodEnd) return 0m;

        var totalDays = sub.PeriodEnd.DayNumber - sub.PeriodStart.DayNumber;
        if (totalDays <= 0) return sub.MonthlyPrice;

        var used = sub.PeriodEnd.DayNumber - activeFrom.DayNumber;
        return Math.Round(sub.MonthlyPrice * used / totalDays, 2);
    }
}
