namespace SrpLab.Warehouse;

public sealed class PickRoutePlanner
{
    public IReadOnlyList<PickStep> PlanWalkingOrder(PickRequest request)
    {
        return request.Lines
            .OrderBy(l => l.Aisle)
            .ThenBy(l => l.Bin)
            .Select(l => new PickStep(l.Aisle, l.Bin, l.Sku, Math.Min(l.QtyNeeded, l.QtyOnHand)))
            .Where(s => s.Qty > 0)
            .ToList();
    }
}
