namespace SrpLab.Ward;

public sealed class WardCensus
{
    private readonly Dictionary<int, BedAssignment> _assignments = new();

    public BedAssignment AssignBed(int bed, string patientId, int acuityScore)
    {
        if (bed <= 0) throw new ArgumentOutOfRangeException(nameof(bed));
        if (string.IsNullOrWhiteSpace(patientId)) throw new ArgumentException("patient required", nameof(patientId));

        var assignment = new BedAssignment(bed, patientId.Trim().ToUpperInvariant(), acuityScore);
        _assignments[bed] = assignment;
        return assignment;
    }

    public BedAssignment? GetAssignment(int bed)
    {
        _assignments.TryGetValue(bed, out var assignment);
        return assignment;
    }

    public IReadOnlyList<BedAssignment> GetAllAssignments()
    {
        return _assignments.Values.OrderBy(a => a.BedNumber).ToList();
    }
}
