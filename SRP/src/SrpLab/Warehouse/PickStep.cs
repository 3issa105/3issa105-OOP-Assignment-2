namespace SrpLab.Warehouse;

public sealed class PickStep
{
    public string Aisle { get; }
    public int Bin { get; }
    public string Sku { get; }
    public int Qty { get; }

    public PickStep(string aisle, int bin, string sku, int qty)
    {
        Aisle = aisle;
        Bin = bin;
        Sku = sku;
        Qty = qty;
    }
}
