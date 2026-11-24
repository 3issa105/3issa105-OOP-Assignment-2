namespace SrpLab.Ward;

public sealed class BedAssignment
{
    public int BedNumber { get; }
    public string PatientId { get; }
    public int AcuityScore { get; }

    public BedAssignment(int bedNumber, string patientId, int acuityScore)
    {
        BedNumber = bedNumber;
        PatientId = patientId;
        AcuityScore = acuityScore;
    }
}
