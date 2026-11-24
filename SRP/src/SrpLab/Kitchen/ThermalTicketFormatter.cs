namespace SrpLab.Kitchen;

public sealed class ThermalTicketFormatter
{
    private const int LineWidth = 32;

    public string Format(int orderNumber, KitchenOrder order, IReadOnlyList<string> allergens, int estimatedMinutes)
    {
        var line = new string('=', LineWidth);
        var body = string.Join('\n', order.Items.Select(i => $"* {i.Name.ToUpperInvariant()} ({i.PrepMinutes}m)"));
        var allergyLine = allergens.Count == 0
            ? "ALLERGENS: none"
            : "ALLERGENS: " + string.Join(",", allergens);

        return $"{line}\nORDER #{orderNumber}\nETA {estimatedMinutes} MIN\n{body}\n{allergyLine}\n{line}\n";
    }
}
