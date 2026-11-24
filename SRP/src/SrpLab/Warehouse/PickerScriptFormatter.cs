namespace SrpLab.Warehouse;

public sealed class PickerScriptFormatter
{
    public string Format(IReadOnlyList<PickStep> steps, IReadOnlyList<PickAllocation> allocations)
    {
        var lines = steps.Select((s, i) => $"{i + 1}. Go aisle {s.Aisle} bin {s.Bin}: pick {s.Qty} x {s.Sku}");
        var shortages = allocations.Where(a => a.Shortfall > 0).Select(a => a.Sku).ToList();
        var warn = shortages.Count > 0
            ? "SHORTAGES: " + string.Join(", ", shortages)
            : "SHORTAGES: none";

        return string.Join('\n', lines) + "\n" + warn;
    }
}
