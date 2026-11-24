namespace SrpLab.Kitchen;

public sealed class PrepTimeEstimator
{
    public int EstimateReadyMinutes(KitchenOrder order, int openStations, bool hasAllergens)
    {
        if (openStations <= 0) openStations = 1;

        var sequential = order.Items.Sum(i => i.PrepMinutes);
        var parallel = (int)Math.Ceiling(sequential / (double)openStations);
        if (hasAllergens)
            parallel += 3;

        var longest = order.Items.Count == 0 ? 0 : order.Items.Max(i => i.PrepMinutes);
        return Math.Max(parallel, longest);
    }
}
