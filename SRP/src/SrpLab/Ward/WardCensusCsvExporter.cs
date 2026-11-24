namespace SrpLab.Ward;

public sealed class WardCensusCsvExporter
{
    public string Export(IEnumerable<BedAssignment> assignments)
    {
        var lines = new List<string> { "bed,patient,acuity" };
        foreach (var item in assignments.OrderBy(a => a.BedNumber))
            lines.Add($"{item.BedNumber},{item.PatientId},{item.AcuityScore}");
        return string.Join('\n', lines);
    }
}
