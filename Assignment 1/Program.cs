using System.Reflection;

static class Program
{
    static void Main(string[] args)
    {
        switch (args[1])
        {
            case "IComparable": TestCompare(); return;
            case "OpOverload": TestOpOverload(); return;
            case "Where": TestWhere(); return;
            case "PlayerWeight": TestPlayerWeight(); return;
            case "WeightChecker": TestWeightChecker(); return;
            default: throw new ArgumentOutOfRangeException($"{args[1]}", $"Unexpected args value: {args[1]}");
        }
    }

    public static void TestCompare()
    {
        Console.WriteLine("=== Checking implementation of IComparable ===");
        Type clsType = typeof(Item);
        Console.WriteLine($"{clsType.Name} implements IComparable<Item>: "
            + typeof(IComparable<Item>).IsAssignableFrom(clsType));

        Console.WriteLine("\n=== Sorting Items ===");
        Player player = new("Aiden", 80);
        List<ItemPickup> itemPickups = [
            new(new("Tunic", 5), "Tristram Cathedral"),
            new(new("Healing Potion", 1), "Tristram Cathedral"),
            new(new("Dagger", 5), "Tristram Cathedral"),
            new(new("Small Shield", 10), "Tristram Cathedral"),
            new(new("Leather Cap", 4), "Tristram Cathedral"),
            new(new("Mace", 10), "Tristram Cathedral"),
            null
        ];

        foreach (var itemPickup in itemPickups)
        {
            player.PickUp(itemPickup);
        }
        player.SortInventory();

        Console.WriteLine("\nSorted by Weight, then by Name:");
        player.PrintInventory();

        Console.WriteLine("\nAdding item and sorting again...");
        player.PickUp(new ItemPickup(new Item("Leather Armor", 10), "The Catacombs"));
        player.SortInventory();
        Console.WriteLine("\nSorted by Weight, then by Name:");
        player.PrintInventory();
    }

    public static void TestOpOverload()
    {
        GoldPickup gp1 = new(1);
        GoldPickup gp2 = new(1);
        GoldPickup gp3 = new(10);
        GoldPickup gp4 = new(10);
        GoldPickup gp5 = new(25);
        GoldPickup gp6 = new(50);

        Console.WriteLine($"gp1 + gp2: {(gp1 + gp2).Value}"); // 2
        Console.WriteLine($"gp2 + gp1: {(gp2 + gp1).Value}"); // 2
        Console.WriteLine($"gp1 + gp3: {(gp1 + gp3).Value}"); // 11
        Console.WriteLine($"gp4 + gp5: {(gp4 + gp5).Value}"); // 35
        Console.WriteLine($"gp5 + gp6: {(gp5 + gp6).Value}"); // 75
    }

    public static void TestWhere()
    {
        Type type = typeof(WeightChecker);
        MethodInfo? method = type.GetMethod("ExceedsWeightThreshold");

        if (method != null && method.IsGenericMethodDefinition)
        {
            Type[] genericArguments = method.GetGenericArguments();

            if (genericArguments.Length == 1)
            {
                Type[] constraints = genericArguments[0].GetGenericParameterConstraints();
                Type[] requiredInterfaces = { typeof(IName), typeof(IWeight) };

                bool hasRequiredConstraints = requiredInterfaces.All(required => constraints.Contains(required));
                if (hasRequiredConstraints)
                {
                    Console.WriteLine("The generic parameter of the method has the required constraints.");
                }
                else
                {
                    Console.WriteLine("The generic parameter of the method does not have the required constraints.");
                }
            }
            else
            {
                Console.WriteLine("Unexpected number of generic parameters.");
            }
        }
        else
        {
            Console.WriteLine("ExceedsWeightThreshold method not found or is not a generic method definition.");
        }
    }

    public static void TestPlayerWeight()
    {
        Player player = new("Moreina", 55);
        Console.WriteLine($"Player {player.Name} weight: {player.Weight}\n");

        List<ItemPickup> itemPickups = [
            new(new("Dagger", 5), "Griswold's Shop"),
            new(new("Small Shield", 10), "Griswold's Shop"),
            new(new("Short Sword", 9), "Griswold's Shop"),
        ];

        foreach (var itemPickup in itemPickups)
        {
            Console.WriteLine($"Player {player.Name} picks up a {itemPickup.Value.Name}");
            player.PickUp(itemPickup);
            Console.WriteLine($"Player {player.Name} weight: {player.Weight}\n");
        }
    }

    public static void TestWeightChecker()
    {
        Console.WriteLine("=== Putting items on traps to see if it triggers ===");
        List<ItemPickup> itemPickups = [
            new(new("Dagger", 5), "Griswold's Shop"),
            new(new("Small Shield", 10), "Griswold's Shop"),
            new(new("Short Sword", 9), "Griswold's Shop"),
        ];

        int trapWeightThreshold = 9; // Trap triggers at weight of over 9
        foreach (var itemPickup in itemPickups)
        {
            if (WeightChecker.ExceedsWeightThreshold(itemPickup.Value, trapWeightThreshold))
                Console.WriteLine("Trap triggered!");
            else
                Console.WriteLine("Trap not triggered.");
            Console.WriteLine();
        }

        Console.WriteLine("=== Players walk over trap. It may trigger... ===");
        trapWeightThreshold = 70;
        List<Player> players = [
            new("Aidan", 80),
            new("Moreina", 55),
            new("Jazreth", 70),
        ];
        foreach (var player in players)
        {
            if (WeightChecker.ExceedsWeightThreshold(player, trapWeightThreshold))
                Console.WriteLine("Trap triggered!");
            else
                Console.WriteLine("Trap not triggered.");
            Console.WriteLine();
        }
    }
}
