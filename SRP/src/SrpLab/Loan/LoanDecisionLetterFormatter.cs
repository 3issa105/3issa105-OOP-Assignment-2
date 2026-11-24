namespace SrpLab.Loan;

public sealed class LoanDecisionLetterFormatter
{
    public string Format(
        string applicantName,
        LoanApplication app,
        decimal riskScore,
        bool isEligible,
        IReadOnlyList<string> requiredDocs)
    {
        if (isEligible)
        {
            return $"Dear {applicantName},\nYour request for {app.RequestedAmount:C} is pre-approved (risk {riskScore:0}).\n" +
                   $"Please upload: {string.Join("; ", requiredDocs)}.\n";
        }

        return $"Dear {applicantName},\nWe are unable to approve {app.RequestedAmount:C} at this time.\n" +
               $"Reference risk={riskScore:0}. You may reapply after improving documentation.\n";
    }
}
