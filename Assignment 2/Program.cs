using System.Reflection;

static class Program
{
    static void Main(string[] args)
    {
        switch (args[1])
        {
            case "Tuple": TestTuple(); return;
            case "JaggedArray": TestJaggedArray(); return;
            case "NDimArray": TestNDimArray(); return;
            default: throw new ArgumentOutOfRangeException($"{args[1]}", $"Unexpected args value: {args[1]}");
        }
    }

    public static void TestTuple()
    {
        CopyingMachine cpm = new("Copying machine 1st floor", (100, 100, 100));
        for (int i = 0; i < 10; i++)
        {
            cpm.Copy();
        }
        var (Cyan, Magenta, Yellow) = cpm.GetPctInk();

        Console.WriteLine(Cyan); // 90
        Console.WriteLine(Magenta); // 90
        Console.WriteLine(Yellow); // 90
    }

    public static void TestJaggedArray()
    {
        Console.WriteLine("=== Checking field and property counts ===");
        PrintAmountOfFieldsAndProperties(typeof(IncidentsReport));

        Console.WriteLine("\n=== Checking type ===");
        PrintFieldType(typeof(IncidentsReport), "Reports", typeof(string[][]));

        Console.WriteLine("\n=== Checking incident reports ===");
        List<Machine> cpms = [
            new CopyingMachine("Copying machine ground floor", (100, 100, 100)),
            new CopyingMachine("Copying machine 1st floor", (100, 100, 100)),
            new CopyingMachine("Copying machine 2nd floor", (100, 100, 100)),
        ];

        for (int i = 0; i < 101; i++)
        {
            (cpms[0] as CopyingMachine)?.Copy(); // 101th Copy() should trigger incident
        }

        cpms[1].AddIncident("Out of paper");

        (cpms[2] as CopyingMachine)?.Copy();
        cpms[2].AddIncident("Paper jam");
        cpms[2].AddIncident("Damaged; possibly heavy object placed on top");

        IncidentsReport report = new(cpms);
        report.PrintReports();
    }

    public static void TestNDimArray()
    {
        Console.WriteLine("=== Checking field and property counts ===");
        PrintAmountOfFieldsAndProperties(typeof(NetworkRouter));

        Console.WriteLine("\n=== Checking type ===");
        PrintFieldType(typeof(NetworkRouter), "Ports", typeof(string[,]));

        Console.WriteLine("\n=== Checking incident reports ===");
        List<Machine> nrs = [
            new NetworkRouter("Cantine router", 2),
            new NetworkRouter("Office router", 3),
            new NetworkRouter("Auditorium router", 3),
        ];

        (nrs[0] as NetworkRouter)?.ConnectDevice(1, 0, "23.73.2.68"); // Should trigger 'device connected' incident
        (nrs[0] as NetworkRouter)?.ConnectDevice(1, 1, "23.73.2.69"); // Should trigger 'device connected' incident
        (nrs[0] as NetworkRouter)?.ConnectDevice(1, 1, "23.73.2.70"); // Should trigger 'already in use' incident
        (nrs[1] as NetworkRouter)?.ConnectDevice(-1, 0, "23.73.2.71"); // Should trigger 'out of range' incident
        (nrs[2] as NetworkRouter)?.ConnectDevice(0, 3, "23.73.2.72"); // Should trigger 'out of range' incident

        IncidentsReport report = new(nrs);
        report.PrintReports();
    }

    private static void PrintAmountOfFieldsAndProperties(Type clsType)
    {
        var fields = clsType.GetFields(
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.Static |
            BindingFlags.FlattenHierarchy);
        var properties = clsType.GetProperties(
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.Static |
            BindingFlags.FlattenHierarchy);

        Console.WriteLine($"{clsType.Name} has {fields.Length} fields and {properties.Length} properties");
    }

    private static void PrintFieldType(Type clsType, string fieldName, Type fieldType)
    {
        FieldInfo? fieldInfo = clsType.GetField(fieldName);
        if (fieldInfo != null)
        {
            if (fieldInfo.FieldType == fieldType)
            {
                Console.WriteLine($"Field '{fieldName}' is of type {fieldType.Name}.");
            }
            else
            {
                Console.WriteLine($"Field '{fieldName}' is NOT of type {fieldType.Name}.");
            }
        }
        else
        {
            Console.WriteLine($"Field '{fieldName}' does not exist.");
        }
    }
}
