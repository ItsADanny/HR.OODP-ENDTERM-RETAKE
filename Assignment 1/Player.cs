class Player : IName, IWeight
{
    public string Name { get; }
    public int BaseWeight { get; private set; }
    public int Weight { get; }
    private readonly List<Item> _inventory = [];
    public int Gold { get; private set; } = 0;
	
	public Player(string name, int baseWeight)
	{
		Name = name;
		BaseWeight = baseWeight;
	}

    public void PickUp() // Add a parameter of type PickUp
    {

    }

// No need to do anything with the below methods
    public void PrintInventory()
    {
        Console.WriteLine("Inventory:");
        foreach (var item in _inventory)
        {
            Console.WriteLine($" - {item.Name} (weight: {item.Weight})");
        }
    }

    public void SortInventory() => _inventory.Sort();
}
