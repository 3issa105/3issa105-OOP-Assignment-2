namespace SrpLab.Kitchen;

public sealed class KitchenOrder
{
    private readonly List<KitchenOrderItem> _items = new();

    public IReadOnlyList<KitchenOrderItem> Items => _items.AsReadOnly();

    public void AddItem(string item, IEnumerable<string> ingredients, int prepMinutes)
    {
        _items.Add(new KitchenOrderItem(item, ingredients, prepMinutes));
    }
}
