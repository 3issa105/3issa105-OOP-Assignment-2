namespace SrpLab.Grades;

public sealed class GradingPolicy
{
    public string GetLetterGrade(decimal average)
    {
        if (average >= 90m) return "A";
        if (average >= 80m) return "B";
        if (average >= 70m) return "C";
        if (average >= 60m) return "D";
        return "F";
    }

    public bool MeetsHonorRoll(decimal average)
    {
        var letter = GetLetterGrade(average);
        return average >= 85m && letter is "A" or "B";
    }
}
