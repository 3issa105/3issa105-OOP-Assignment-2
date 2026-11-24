namespace SrpLab.Ward;

public sealed class HandoffNoteFormatter
{
    public string Format(int bed, BedAssignment? assignment)
    {
        if (assignment is null)
            return $"Bed {bed}: empty";

        var tone = assignment.AcuityScore >= 8 ? "ESCALATE" : assignment.AcuityScore >= 4 ? "WATCH" : "STABLE";
        return $"[HANDOFF {DateTime.UtcNow:yyyy-MM-dd}] Bed {assignment.BedNumber} | {assignment.PatientId} | acuity={assignment.AcuityScore} | {tone}";
    }
}
