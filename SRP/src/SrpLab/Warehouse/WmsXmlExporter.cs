namespace SrpLab.Warehouse;

public sealed class WmsXmlExporter
{
    public string ExportBatch(string batchId, IReadOnlyList<PickAllocation> allocations)
    {
        var parts = allocations.Select(a => $"<line sku=\"{a.Sku}\" qty=\"{a.Allocated}\" />");
        return $"<batch id=\"{batchId}\">{string.Join("", parts)}</batch>";
    }
}
