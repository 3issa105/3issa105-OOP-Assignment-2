namespace SrpLab.Warehouse;

public sealed class PickRequest
{
    private readonly List<PickRequestLine> _lines = new();

    public IReadOnlyList<PickRequestLine> Lines => _lines.AsReadOnly();

    public void AddNeed(string sku, string aisle, int bin, int qtyNeeded, int qtyOnHand)
    {
        _lines.Add(new PickRequestLine(sku, aisle, bin, qtyNeeded, qtyOnHand));
    }
}
