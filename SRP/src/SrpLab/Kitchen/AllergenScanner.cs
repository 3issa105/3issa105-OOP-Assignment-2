namespace SrpLab.Kitchen;

public sealed class AllergenScanner
{
    public IReadOnlyList<string> DetectAllergens(KitchenOrder order)
    {
        var hits = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var item in order.Items)
        {
            foreach (var ing in item.Ingredients)
            {
                if (ing.Contains("milk") || ing.Contains("cheese") || ing.Contains("butter")) hits.Add("dairy");
                if (ing.Contains("wheat") || ing.Contains("flour") || ing.Contains("bread")) hits.Add("gluten");
                if (ing.Contains("peanut") || ing.Contains("almond") || ing.Contains("cashew")) hits.Add("nuts");
                if (ing.Contains("shrimp") || ing.Contains("prawn") || ing.Contains("crab")) hits.Add("shellfish");
            }
        }

        return hits.OrderBy(x => x).ToList();
    }
}
