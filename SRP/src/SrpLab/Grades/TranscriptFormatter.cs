namespace SrpLab.Grades;

public sealed class TranscriptFormatter
{
    public string FormatPlain(string studentId, string fullName, decimal average, string letter, bool meetsHonorRoll)
    {
        return $"TRANSCRIPT\nStudent: {fullName} ({studentId})\nAverage: {average}\nLetter: {letter}\nHonor: {meetsHonorRoll}\n";
    }
}
