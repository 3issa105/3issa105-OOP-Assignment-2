namespace SrpLab.Loan;

public sealed class UnderwriterCsvExporter
{
    public string FormatRow(string applicationId, LoanApplication app, decimal riskScore, bool isEligible)
    {
        return $"{applicationId},{app.CreditScore},{app.EmploymentMonths},{(app.HasCollateral ? 1 : 0)},{riskScore:0.00},{(isEligible ? "Y" : "N")}";
    }
}
