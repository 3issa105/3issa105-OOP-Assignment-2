namespace PatternsLab.Problems.Prototype;

public class EnemyPrototypeRegistry
{
    private readonly Dictionary<string, Enemy> _prototypes = new(StringComparer.OrdinalIgnoreCase);

    public void Register(string key, Enemy prototype)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Prototype key is required.", nameof(key));
        if (prototype is null)
            throw new ArgumentNullException(nameof(prototype));

        _prototypes[key.Trim()] = prototype;
    }

    public Enemy Create(string key)
    {
        if (!_prototypes.TryGetValue(key, out var prototype))
            throw new KeyNotFoundException($"No enemy prototype registered under '{key}'.");

        return prototype.Clone();
    }
}
