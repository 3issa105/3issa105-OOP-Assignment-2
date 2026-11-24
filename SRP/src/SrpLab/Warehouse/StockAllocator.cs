namespace SrpLab.Warehouse;

public sealed class StockAllocator
{
    public IReadOnlyList<PickAllocation> Allocate(PickRequest request)
    {
        var result = new List<PickAllocation>();
        foreach (var line in request.Lines)
        {
            var allocated = Math.Min(line.QtyNeeded, line.QtyOnHand);
            var shortfall = Math.Max(0, line.QtyNeeded - allocated);
            result.Add(new PickAllocation(line.Sku, allocated, shortfall));
        }
        return result;
    }
}
