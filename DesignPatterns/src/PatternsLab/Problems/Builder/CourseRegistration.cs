namespace PatternsLab.Problems.Builder;

public sealed class CourseRegistration
{
    public string StudentEmail { get; }
    public string CourseCode { get; }
    public string AccessMode { get; }
    public string? GroupCode { get; }
    public string? DiscountCode { get; }
    public bool SendWhatsApp { get; }
    public bool SendEmailWelcome { get; }
    public string? MentorNote { get; }
    public DateOnly? PreferredStart { get; }

    internal CourseRegistration(
        string studentEmail,
        string courseCode,
        string accessMode,
        string? groupCode,
        string? discountCode,
        bool sendWhatsApp,
        bool sendEmailWelcome,
        string? mentorNote,
        DateOnly? preferredStart)
    {
        StudentEmail = studentEmail;
        CourseCode = courseCode;
        AccessMode = accessMode;
        GroupCode = groupCode;
        DiscountCode = discountCode;
        SendWhatsApp = sendWhatsApp;
        SendEmailWelcome = sendEmailWelcome;
        MentorNote = mentorNote;
        PreferredStart = preferredStart;
    }

    public override string ToString()
        => $"{StudentEmail} -> {CourseCode} [{AccessMode}] group={GroupCode ?? "-"} discount={DiscountCode ?? "-"} wa={SendWhatsApp} mail={SendEmailWelcome}";
}

public sealed class CourseRegistrationBuilder
{
    private readonly string _studentEmail;
    private readonly string _courseCode;
    private readonly string _accessMode;
    private string? _groupCode;
    private string? _discountCode;
    private bool _sendWhatsApp;
    private bool _sendEmailWelcome;
    private string? _mentorNote;
    private DateOnly? _preferredStart;

    public CourseRegistrationBuilder(string studentEmail, string courseCode, string accessMode)
    {
        _studentEmail = studentEmail;
        _courseCode = courseCode;
        _accessMode = accessMode;
    }

    public CourseRegistrationBuilder WithGroupCode(string? groupCode)
    {
        _groupCode = groupCode;
        return this;
    }

    public CourseRegistrationBuilder WithDiscountCode(string? discountCode)
    {
        _discountCode = discountCode;
        return this;
    }

    public CourseRegistrationBuilder WithWhatsApp(bool send = true)
    {
        _sendWhatsApp = send;
        return this;
    }

    public CourseRegistrationBuilder WithEmailWelcome(bool send = true)
    {
        _sendEmailWelcome = send;
        return this;
    }

    public CourseRegistrationBuilder WithMentorNote(string? mentorNote)
    {
        _mentorNote = mentorNote;
        return this;
    }

    public CourseRegistrationBuilder WithPreferredStart(DateOnly? preferredStart)
    {
        _preferredStart = preferredStart;
        return this;
    }

    public CourseRegistration Build()
    {
        if (string.IsNullOrWhiteSpace(_studentEmail))
            throw new ArgumentException("Student email is required.");
        if (string.IsNullOrWhiteSpace(_courseCode))
            throw new ArgumentException("Course code is required.");
        if (_accessMode != "LiveGroup" && _accessMode != "VideosOnly")
            throw new InvalidOperationException($"Unsupported access mode '{_accessMode}'. Expected 'LiveGroup' or 'VideosOnly'.");
        if (_accessMode == "LiveGroup" && string.IsNullOrWhiteSpace(_groupCode))
            throw new InvalidOperationException("LiveGroup registration requires a GroupCode.");
        if (_accessMode == "VideosOnly" && !string.IsNullOrWhiteSpace(_groupCode))
            throw new InvalidOperationException("VideosOnly registration cannot have a GroupCode.");

        return new CourseRegistration(
            _studentEmail.Trim(),
            _courseCode.Trim(),
            _accessMode,
            string.IsNullOrWhiteSpace(_groupCode) ? null : _groupCode.Trim(),
            string.IsNullOrWhiteSpace(_discountCode) ? null : _discountCode.Trim(),
            _sendWhatsApp,
            _sendEmailWelcome,
            _mentorNote,
            _preferredStart);
    }
}

public static class RegistrationCallSites
{
    public static CourseRegistration CreateLiveStudent()
    {
        return new CourseRegistrationBuilder("sara@mail.com", "SEF-101", "LiveGroup")
            .WithGroupCode("G1")
            .WithDiscountCode("EARLY10")
            .WithWhatsApp()
            .WithEmailWelcome()
            .WithMentorNote("Needs evening slot")
            .WithPreferredStart(new DateOnly(2026, 10, 1))
            .Build();
    }

    public static CourseRegistration CreateVideosOnly()
    {
        return new CourseRegistrationBuilder("ali@mail.com", "SEF-101", "VideosOnly")
            .WithEmailWelcome()
            .Build();
    }
}
