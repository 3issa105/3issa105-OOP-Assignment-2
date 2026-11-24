namespace SrpLab.Kitchen;

public sealed class KitchenOrderItem
{
    public string Name { get; }
    public IReadOnlyList<string> Ingredients { get; }
    public int PrepMinutes { get; }

    public KitchenOrderItem(string name, IEnumerable<string> ingredients, int prepMinutes)
    {
        Name = name;
        Ingredients = ingredients.Select(i => i.Trim().ToLowerInvariant()).ToList();
        PrepMinutes = prepMinutes;
    }
}
