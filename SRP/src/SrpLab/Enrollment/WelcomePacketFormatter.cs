namespace SrpLab.Enrollment;

public sealed class WelcomePacketFormatter
{
    public string FormatMarkdown(CourseRoster roster, string studentEmail, string studentName)
    {
        var status = roster.IsSeated(studentEmail)
            ? "confirmed seat"
            : $"waitlist #{roster.WaitlistPosition(studentEmail)}";

        return $"# Welcome to {roster.CourseCode}\nHi {studentName},\nYour status: **{status}**.\n" +
               $"Bring a laptop. Discord onboarding link: https://example.invalid/{roster.CourseCode.ToLowerInvariant()}\n";
    }
}
