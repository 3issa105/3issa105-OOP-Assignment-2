namespace SrpLab.Loan;

public sealed class LoanRiskEvaluator
{
    public decimal CalculateRiskScore(LoanApplication app)
    {
        decimal score = 100m;
        score -= Math.Max(0, 700 - app.CreditScore) * 0.15m;
        if (app.EmploymentMonths < 6) score -= 20m;
        if (app.RequestedAmount > 50_000m && !app.HasCollateral) score -= 25m;
        if (app.RequestedAmount > 150_000m) score -= 10m;
        return Math.Clamp(score, 0m, 100m);
    }

    public bool IsEligible(LoanApplication app)
    {
        return CalculateRiskScore(app) >= 55m && app.CreditScore >= 580;
    }
}
