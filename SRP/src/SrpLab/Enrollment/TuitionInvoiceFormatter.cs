namespace SrpLab.Enrollment;

public sealed class TuitionInvoiceFormatter
{
    private const decimal VatRate = 0.14m;

    public string FormatLine(CourseRoster roster, string studentEmail)
    {
        if (!roster.IsSeated(studentEmail))
            return $"{roster.CourseCode},WAITLIST,0.00";

        var vat = Math.Round(roster.Tuition * VatRate, 2);
        return $"{roster.CourseCode},TUITION,{roster.Tuition:0.00},VAT,{vat:0.00},TOTAL,{(roster.Tuition + vat):0.00}";
    }
}
