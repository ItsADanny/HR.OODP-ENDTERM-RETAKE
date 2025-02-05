using System.Reflection;

static class Program
{
    static void Main(string[] args)
    {
        switch (args[1])
        {
            case "Enum": TestEnum(); return;
            case "Dictionary": TestDictionary(); return;
            case "Filter": TestFilterApplicants(); return;
            case "Assemble": TestAssemble(); return;
            case "Invite": TestInvite(); return;
            default: throw new ArgumentOutOfRangeException($"{args[1]}", $"Unexpected args value: {args[1]}");
        }
    }

    public static void TestEnum()
    {
        List<TeachingCertificate> certificates = Enum.GetValues(typeof(TeachingCertificate)).Cast<TeachingCertificate>().ToList();
        Console.WriteLine("Printing all certificates:");
        foreach (TeachingCertificate certificate in certificates)
        {
            Console.WriteLine($" - {certificate}");
        }
    }

    public static void TestDictionary()
    {
        foreach (var keyValue in ApplicantManager.CertificateInfo)
        {
            Console.WriteLine($"{keyValue.Key}: {keyValue.Value}");
        }
    }

    public static void TestFilterApplicants()
    {
        Console.WriteLine("=== Test 1 ===");
        List<Applicant> applicants = [
            new("Alice", 30, TeachingCertificate.BDB1, 3),
            new("Bob", 45, TeachingCertificate.BKO, 10),
            new("Charlie", 25, TeachingCertificate.None, 1),
            new("Diana", 38, TeachingCertificate.SKO, 15),
            new("Eve", 28, TeachingCertificate.BDB2, 2),
            new("Frank", 50, TeachingCertificate.SKO, 20),
            new("Grace", 35, TeachingCertificate.BKO, 8),
            new("Hank", 29, TeachingCertificate.None, 5),
            new("Ivy", 32, TeachingCertificate.BDB1, 6),
            new("Jack", 40, TeachingCertificate.BDB2, 7),
        ];
        ApplicantManager manager = new(applicants);
        manager.FilterApplicants(4, TeachingCertificate.BDB1);
        PrintApplicants(manager.Applicants);

        Console.WriteLine("\n=== Test 2 ===");
        applicants = [
            new("Alice", 30, TeachingCertificate.BDB2, 4),
            new("Bob", 45, TeachingCertificate.SKO, 12),
            new("Charlie", 25, TeachingCertificate.BDB1, 2),
            new("Diana", 38, TeachingCertificate.BKO, 11),
            new("Eve", 28, TeachingCertificate.None, 1),
            new("Frank", 50, TeachingCertificate.BDB2, 10),
            new("Grace", 35, TeachingCertificate.BKO, 9),
            new("Hank", 29, TeachingCertificate.SKO, 7),
            new("Ivy", 32, TeachingCertificate.None, 3),
            new("Jack", 40, TeachingCertificate.BKO, 5),
        ];
        manager = new(applicants);
        manager.FilterApplicants(7, TeachingCertificate.None);
        PrintApplicants(manager.Applicants);

        Console.WriteLine("\n=== Test 3 ===");
        applicants = [
            new("Zara", 26, TeachingCertificate.BDB1, 2),
            new("Liam", 42, TeachingCertificate.SKO, 15),
            new("Noah", 31, TeachingCertificate.BKO, 7),
            new("Olivia", 27, TeachingCertificate.None, 3),
            new("Emma", 36, TeachingCertificate.BDB2, 10),
            new("Ava", 29, TeachingCertificate.BDB1, 6),
            new("Sophia", 44, TeachingCertificate.SKO, 18),
            new("James", 33, TeachingCertificate.None, 4),
            new("Mia", 24, TeachingCertificate.BDB2, 1),
            new("Elijah", 39, TeachingCertificate.BKO, 11),
        ];
        manager = new(applicants);
        manager.FilterApplicants(4, TeachingCertificate.BDB1);
        PrintApplicants(manager.Applicants);

        Console.WriteLine("\n=== Test 3 ===");
        applicants = [
            new("Alice", 30, TeachingCertificate.BDB1, 5),
            new("Bob", 45, TeachingCertificate.BKO, 10),
            new("Charlie", 25, TeachingCertificate.None, 1),
            new("Diana", 38, TeachingCertificate.SKO, 15),
            new("Eve", 28, TeachingCertificate.BDB2, 2),
            new("Frank", 50, TeachingCertificate.SKO, 20),
            new("Grace", 35, TeachingCertificate.BKO, 8),
            new("Hank", 29, TeachingCertificate.None, 5),
            new("Ivy", 32, TeachingCertificate.BDB1, 6),
            new("Jack", 40, TeachingCertificate.BDB2, 7),
        ];
        manager = new(applicants);
        manager.FilterApplicants(4, TeachingCertificate.BDB1);
        PrintApplicants(manager.Applicants);

        Console.WriteLine("\n=== Test 4 ===");
        applicants = [
            new("Alice", 30, TeachingCertificate.BDB2, 4),
            new("Bob", 45, TeachingCertificate.SKO, 12),
            new("Charlie", 25, TeachingCertificate.BDB1, 2),
            new("Diana", 38, TeachingCertificate.BKO, 11),
            new("Eve", 28, TeachingCertificate.None, 1),
            new("Frank", 50, TeachingCertificate.BDB2, 10),
            new("Grace", 35, TeachingCertificate.BKO, 9),
            new("Hank", 29, TeachingCertificate.SKO, 7),
            new("Ivy", 32, TeachingCertificate.None, 3),
            new("Jack", 40, TeachingCertificate.BKO, 5),
        ];
        manager = new(applicants);
        manager.FilterApplicants(4, (TeachingCertificate.BDB1));
        PrintApplicants(manager.Applicants);

        Console.WriteLine("\n=== Test 5 ===");
        applicants = [
            new("Zara", 26, TeachingCertificate.BDB1, 2),
            new("Liam", 42, TeachingCertificate.SKO, 15),
            new("Noah", 31, TeachingCertificate.BKO, 7),
            new("Olivia", 27, TeachingCertificate.None, 3),
            new("Emma", 36, TeachingCertificate.BDB2, 10),
            new("Ava", 29, TeachingCertificate.BDB1, 6),
            new("Sophia", 44, TeachingCertificate.SKO, 18),
            new("James", 33, TeachingCertificate.None, 4),
            new("Mia", 24, TeachingCertificate.BDB2, 1),
            new("Elijah", 39, TeachingCertificate.BKO, 11),
        ];
        manager = new(applicants);
        manager.FilterApplicants(8, (TeachingCertificate.BDB2));
        PrintApplicants(manager.Applicants);
    }

    public static void TestAssemble()
    {
        PrintQueueType();

        Console.WriteLine("\n=== Test 1 ===");
        List<Applicant> applicants = [
            new("Grace", 36, TeachingCertificate.BKO, 8),
            new("Hank", 29, TeachingCertificate.SKO, 15),
            new("Ivy", 32, TeachingCertificate.BDB1, 6),
            new("Jack", 40, TeachingCertificate.BDB2, 7),
            new("Eve", 28, TeachingCertificate.BDB2, 2),
            new("Grace", 35, TeachingCertificate.BKO, 8),
            new("Hank", 15, TeachingCertificate.None, 5),
            new("Charlie", 25, TeachingCertificate.None, 1),
            new("Alice", 30, TeachingCertificate.BDB1, 3),
            new("Frank", 50, TeachingCertificate.SKO, 20),
            new("Bob", 45, TeachingCertificate.BKO, 10),
            new("Bob", 50, TeachingCertificate.BDB1, 1),
            new("Diana", 38, TeachingCertificate.SKO, 15),
        ];
        ApplicantManager manager = new(applicants);
        Console.WriteLine($"Queue count before assembling: {manager.ToInvite.Count}");
        manager.AssembleApplicantsByNameAndAge();
        Console.WriteLine($"Queue count after assembling: {manager.ToInvite.Count}");
        PrintApplicants(manager.ToInvite);

        Console.WriteLine("\n=== Test 2 ===");
        applicants = [
            new("Grace", 36, TeachingCertificate.BKO, 8),
            new("Hank", 29, TeachingCertificate.SKO, 15),
            new("Ivy", 32, TeachingCertificate.BDB1, 6),
            new("Jack", 40, TeachingCertificate.BDB2, 7),
            new("Grace", 35, TeachingCertificate.BKO, 8),
            new("Hank", 15, TeachingCertificate.None, 5),
            new("Charlie", 25, TeachingCertificate.None, 1),
            new("Alice", 30, TeachingCertificate.BDB1, 3),
            new("Charlie", 35, TeachingCertificate.SKO, 1),
            new("Frank", 50, TeachingCertificate.SKO, 20),
            new("Bob", 45, TeachingCertificate.BKO, 10),
            new("Bob", 50, TeachingCertificate.BDB1, 1),
            new("Diana", 38, TeachingCertificate.SKO, 15),
        ];
        manager = new(applicants);
        Console.WriteLine($"Queue count before assembling: {manager.ToInvite.Count}");
        manager.AssembleApplicantsByNameAndAge();
        Console.WriteLine($"Queue count after assembling: {manager.ToInvite.Count}");
        PrintApplicants(manager.ToInvite);

        Console.WriteLine("\n=== Test 3 ===");
        applicants = [
            new("Grace", 36, TeachingCertificate.BKO, 8),
            new("Hank", 29, TeachingCertificate.SKO, 15),
            new("Ivy", 32, TeachingCertificate.BDB1, 6),
            new("Jack", 40, TeachingCertificate.BDB2, 7),
            new("Eve", 28, TeachingCertificate.BDB2, 2),
            new("Grace", 35, TeachingCertificate.BKO, 8),
            new("Hank", 15, TeachingCertificate.None, 5),
            new("Charlie", 25, TeachingCertificate.None, 1),
            new("Alice", 30, TeachingCertificate.BDB1, 3),
            new("Alice", 31, TeachingCertificate.BDB1, 3),
            new("Frank", 50, TeachingCertificate.SKO, 20),
            new("Bob", 45, TeachingCertificate.BKO, 10),
            new("Bob", 50, TeachingCertificate.BDB1, 1),
            new("Diana", 38, TeachingCertificate.SKO, 15),
        ];
        manager = new(applicants);
        Console.WriteLine($"Queue count before assembling: {manager.ToInvite.Count}");
        manager.AssembleApplicantsByNameAndAge();
        Console.WriteLine($"Queue count after assembling: {manager.ToInvite.Count}");
        PrintApplicants(manager.ToInvite);
    }

    public static void TestInvite()
    {
        PrintQueueType();

        Console.WriteLine("\n=== Test 1 ===");
        List<Applicant> applicants = [
            new("Grace", 36, TeachingCertificate.BKO, 8),
            new("Hank", 29, TeachingCertificate.SKO, 15),
            new("Ivy", 32, TeachingCertificate.BDB1, 6),
            new("Jack", 40, TeachingCertificate.BDB2, 7),
            new("Eve", 28, TeachingCertificate.BDB2, 2),
            new("Grace", 35, TeachingCertificate.BKO, 8),
            new("Hank", 15, TeachingCertificate.None, 5),
            new("Charlie", 25, TeachingCertificate.None, 1),
            new("Alice", 30, TeachingCertificate.BDB1, 3),
            new("Frank", 50, TeachingCertificate.SKO, 20),
            new("Bob", 45, TeachingCertificate.BKO, 10),
            new("Bob", 50, TeachingCertificate.BDB1, 1),
            new("Diana", 38, TeachingCertificate.SKO, 15),
        ];
        ApplicantManager manager = new(applicants);
        Console.WriteLine($"Queue count before assembling: {manager.ToInvite.Count}");
        manager.AssembleApplicantsByNameAndAge();
        Console.WriteLine($"Queue count after assembling: {manager.ToInvite.Count}");
        manager.InviteApplicants();
        Console.WriteLine($"Queue count after inviting: {manager.ToInvite.Count}");

        Console.WriteLine("\n=== Test 2 ===");
        applicants = [
            new("Grace", 36, TeachingCertificate.BKO, 8),
            new("Hank", 29, TeachingCertificate.SKO, 15),
            new("Ivy", 32, TeachingCertificate.BDB1, 6),
            new("Jack", 40, TeachingCertificate.BDB2, 7),
            new("Grace", 35, TeachingCertificate.BKO, 8),
            new("Hank", 15, TeachingCertificate.None, 5),
            new("Charlie", 25, TeachingCertificate.None, 1),
            new("Alice", 30, TeachingCertificate.BDB1, 3),
            new("Charlie", 35, TeachingCertificate.SKO, 1),
            new("Frank", 50, TeachingCertificate.SKO, 20),
            new("Bob", 45, TeachingCertificate.BKO, 10),
            new("Bob", 50, TeachingCertificate.BDB1, 1),
            new("Diana", 38, TeachingCertificate.SKO, 15),
        ];
        manager = new(applicants);
        Console.WriteLine($"Queue count before assembling: {manager.ToInvite.Count}");
        manager.AssembleApplicantsByNameAndAge();
        Console.WriteLine($"Queue count after assembling: {manager.ToInvite.Count}");
        manager.InviteApplicants();
        Console.WriteLine($"Queue count after inviting: {manager.ToInvite.Count}");

        Console.WriteLine("\n=== Test 3 ===");
        applicants = [
            new("Grace", 36, TeachingCertificate.BKO, 8),
            new("Hank", 29, TeachingCertificate.SKO, 15),
            new("Ivy", 32, TeachingCertificate.BDB1, 6),
            new("Jack", 40, TeachingCertificate.BDB2, 7),
            new("Eve", 28, TeachingCertificate.BDB2, 2),
            new("Grace", 35, TeachingCertificate.BKO, 8),
            new("Hank", 15, TeachingCertificate.None, 5),
            new("Charlie", 25, TeachingCertificate.None, 1),
            new("Alice", 30, TeachingCertificate.BDB1, 3),
            new("Alice", 31, TeachingCertificate.BDB1, 3),
            new("Frank", 50, TeachingCertificate.SKO, 20),
            new("Bob", 45, TeachingCertificate.BKO, 10),
            new("Bob", 50, TeachingCertificate.BDB1, 1),
            new("Diana", 38, TeachingCertificate.SKO, 15),
        ];
        manager = new(applicants);
        Console.WriteLine($"Queue count before assembling: {manager.ToInvite.Count}");
        manager.AssembleApplicantsByNameAndAge();
        Console.WriteLine($"Queue count after assembling: {manager.ToInvite.Count}");
        manager.InviteApplicants();
        Console.WriteLine($"Queue count after inviting: {manager.ToInvite.Count}");
    }

    private static void PrintApplicants(IEnumerable<Applicant> applicants)
    {
        foreach (var applicant in applicants)
        {
            Console.WriteLine($"{applicant.Name} ({applicant.Age} years old)");
            Console.WriteLine($" - Certificate: {applicant.Certificate}; years experience: {applicant.YearsExperience}");
        }
    }

    private static void PrintQueueType()
    {
        string propertyName = "ToInvite";
        Type managerType = typeof(ApplicantManager);
        PropertyInfo info = managerType.GetProperty(propertyName,
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.Static);

        Type type = info.PropertyType;
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Queue<>))
        {
            Type[] genericArguments = type.GetGenericArguments();
            string genericTypeName = genericArguments[0].Name; // Applicant
            Console.WriteLine($"Property {propertyName} is a Queue of type {genericTypeName}");
        }
        else
        {
            Console.WriteLine($"Property {propertyName} is not a Queue");
        }
    }
}
