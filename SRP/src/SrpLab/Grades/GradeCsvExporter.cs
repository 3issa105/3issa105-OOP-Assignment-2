namespace SrpLab.Grades;

public sealed class GradeCsvExporter
{
    public string Export(ScoreBook scores, GradingPolicy policy)
    {
        var rows = new List<string> { "studentId,average,letter,honor" };
        foreach (var id in scores.GetStudentIds())
        {
            var avg = scores.Average(id);
            var letter = policy.GetLetterGrade(avg);
            var honor = policy.MeetsHonorRoll(avg) ? 1 : 0;
            rows.Add($"{id},{avg},{letter},{honor}");
        }
        return string.Join('\n', rows);
    }
}
