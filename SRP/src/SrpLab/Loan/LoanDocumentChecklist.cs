namespace SrpLab.Loan;

public sealed class LoanDocumentChecklist
{
    public IReadOnlyList<string> GetRequiredDocuments(LoanApplication app, bool isEligible)
    {
        var docs = new List<string> { "National ID", "Proof of income (3 months)" };
        if (app.RequestedAmount > 40_000m) docs.Add("Bank statements (6 months)");
        if (app.HasCollateral) docs.Add("Collateral ownership deed");
        if (app.EmploymentMonths < 12) docs.Add("Employer letter");
        if (!isEligible) docs.Add("Manual underwriter referral form");
        return docs;
    }
}
