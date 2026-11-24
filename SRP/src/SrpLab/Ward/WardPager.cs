namespace SrpLab.Ward;

public sealed class WardPager
{
    private readonly List<string> _pagerLog = new();

    public void Evaluate(int bed, int acuityScore)
    {
        if (acuityScore >= 8)
            _pagerLog.Add($"CODE-YELLOW bed={bed} at {DateTime.UtcNow:HH:mm}");
    }

    public IReadOnlyList<string> DrainLog()
    {
        var copy = _pagerLog.ToList();
        _pagerLog.Clear();
        return copy;
    }
}
