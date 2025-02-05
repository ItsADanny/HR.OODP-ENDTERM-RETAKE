abstract class Machine
{
    public readonly string Name;
    public readonly int Id;
    public static int Counter = 0;
    public readonly List<string> Incidents = [];

    public Machine(string name)
    {
        Name = name;
        Id = ++Counter;
    }

    public virtual void AddIncident(string description)
    {
        Incidents.Add($"Machine name: {Name}\n - {description}");
    }
}
