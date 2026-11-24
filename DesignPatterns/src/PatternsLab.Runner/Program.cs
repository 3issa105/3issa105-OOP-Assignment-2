using System.Collections.Concurrent;
using System.Diagnostics;
using PatternsLab.Problems.Builder;
using PatternsLab.Problems.Prototype;
using PatternsLab.Problems.Singleton;

Console.WriteLine("=== SINGLETON: AFTER ===\n");

// var direct = new AppConfig(); // must NOT compile: constructor is private

var db = new DatabaseService();
var ui = new UiService();

db.Config.Theme = "Dark";
Console.WriteLine("Admin changed theme to Dark.");
ui.Render();

Console.WriteLine($"Same config object? {ReferenceEquals(db.Config, ui.Config)}");
Console.WriteLine($"Times config was loaded from disk: {AppConfig.LoadCount}");

var threadRefs = new ConcurrentBag<AppConfig>();
Parallel.For(0, 8, _ => threadRefs.Add(AppConfig.Instance));
bool allSameRef = threadRefs.All(c => ReferenceEquals(c, AppConfig.Instance));
Console.WriteLine($"Thread-safe across 8 parallel reads? {allSameRef} (LoadCount={AppConfig.LoadCount})");

Console.WriteLine("\n=== PROTOTYPE: AFTER ===\n");

var registry = new EnemyPrototypeRegistry();
Enemy orcPrototype = new Orc();
orcPrototype.Name = "Boss Orc";
registry.Register("orc", orcPrototype);
registry.Register("elf", new Elf());

var sw = Stopwatch.StartNew();
var army = new List<Enemy>();
for (int i = 1; i <= 5; i++)
{
    Enemy clone = registry.Create("orc");
    clone.Name = $"Orc-{i}";
    army.Add(clone);
}
sw.Stop();
Console.WriteLine($"Cloned 5 orcs in {sw.ElapsedMilliseconds} ms");

Enemy copy = orcPrototype.Clone();
Console.WriteLine($"\nOriginal model id: {orcPrototype.ModelId}");
Console.WriteLine($"Copy model id:     {copy.ModelId}");

copy.Weapon.Damage = 999;
copy.Abilities.Add("Fire Breath");
Console.WriteLine("\nMutated copy's weapon damage to 999 and added Fire Breath.");
Console.WriteLine($"Original weapon damage: {orcPrototype.Weapon.Damage}");
Console.WriteLine($"Copy weapon damage:     {copy.Weapon.Damage}");
Console.WriteLine($"Original abilities:     {string.Join(", ", orcPrototype.Abilities)}");
Console.WriteLine($"Copy abilities:         {string.Join(", ", copy.Abilities)}");

Enemy elfClone = registry.Create("elf");
Console.WriteLine($"Cloned from registry: {elfClone.Name}, Weapon={elfClone.Weapon.Name}, ModelId={elfClone.ModelId}");

Console.WriteLine("\n=== BUILDER: AFTER ===\n");
Console.WriteLine(RegistrationCallSites.CreateLiveStudent());
Console.WriteLine(RegistrationCallSites.CreateVideosOnly());

try
{
    new CourseRegistrationBuilder("test@mail.com", "SEF-101", "LiveGroup").Build();
}
catch (Exception ex)
{
    Console.WriteLine($"Expected LiveGroup validation error: {ex.Message}");
}

try
{
    new CourseRegistrationBuilder("test@mail.com", "SEF-101", "VideosOnly")
        .WithGroupCode("G2")
        .Build();
}
catch (Exception ex)
{
    Console.WriteLine($"Expected VideosOnly validation error: {ex.Message}");
}
