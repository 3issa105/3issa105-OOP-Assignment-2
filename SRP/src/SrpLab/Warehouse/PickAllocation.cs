namespace SrpLab.Warehouse;

public sealed class PickAllocation
{
    public string Sku { get; }
    public int Allocated { get; }
    public int Shortfall { get; }

    public PickAllocation(string sku, int allocated, int shortfall)
    {
        Sku = sku;
        Allocated = allocated;
        Shortfall = shortfall;
    }
}
