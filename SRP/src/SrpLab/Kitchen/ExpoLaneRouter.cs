namespace SrpLab.Kitchen;

public sealed class ExpoLaneRouter
{
    public string DetermineLane(bool hasAllergens, int estimatedMinutes)
    {
        if (hasAllergens) return "LANE-ALLERGY";
        return estimatedMinutes > 20 ? "LANE-SLOW" : "LANE-FAST";
    }
}
