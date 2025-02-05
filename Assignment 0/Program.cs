using System.Reflection;

static class Program
{
    static void Main(string[] args)
    {
        switch (args[1])
        {
            case "ReadOnly": TestReadOnlyFields(); return;
            case "Constant": TestConstFields(); return;
            case "Interface": TestInterface(); return;
            case "Abstract": TestAbstract(); return;
            case "Encapsulation": TestEncapsulation(); return;
            case "FunctProductElectronics": TestFunctProductElectronics(); return;
            case "FunctCreditCardPayment": TestFunctCreditCardPayment(); return;
            case "FunctPaymentProcessor": TestPaymentProcessor(); return;
            default: throw new ArgumentOutOfRangeException($"{args[1]}", $"Unexpected args value: {args[1]}");
        }
    }

    public static void TestReadOnlyFields()
    {
        Type clsType = typeof(Product);
        PrintIsFieldReadOnly(clsType, "Name");
        PrintIsFieldReadOnly(clsType, "BasePrice");

        clsType = typeof(Electronics);
        PrintIsFieldReadOnly(clsType, "_instructions");

        clsType = typeof(CreditCardPayment);
        PrintIsFieldReadOnly(clsType, "CardNumber");
    }

    public static void TestConstFields()
    {
        Type clsType = typeof(Electronics);
        PrintIsFieldConstant(clsType, "TaxRate");
    }

    public static void TestInterface()
    {
        Type interfaceType = typeof(IPayment);
        Console.WriteLine("Is an Interface: " + interfaceType.IsInterface);
        
    // Check whether the class(es) have implemented the interface
        Type implementingType = typeof(CreditCardPayment);
        Console.WriteLine($"Is implemented by {implementingType}: "
            + interfaceType.IsAssignableFrom(implementingType));

    // Check whether the members have the correct signatures
        string propertyName = "Amount";
        Console.WriteLine($"Property {propertyName} has the correct type and the correct get/set accessors: "
            + CheckPropertySignature(interfaceType, propertyName, typeof(double), true, true));
        propertyName = "HolderName";
        Console.WriteLine($"Property {propertyName} has the correct type and the correct get/set accessors: "
            + CheckPropertySignature(interfaceType, propertyName, typeof(string), true, false));
        propertyName = "IsProcessed";
        Console.WriteLine($"Property {propertyName} has the correct type and the correct get/set accessors: "
            + CheckPropertySignature(interfaceType, propertyName, typeof(bool), true, false));
        string methodName = "ProcessPayment";
        Console.WriteLine($"Method {methodName} has the correct return and parameter types: "
            + CheckMethodSignature(interfaceType, methodName, typeof(void), []));
    }

    public static void TestAbstract()
    {
        Type baseType = typeof(Product);

        Console.WriteLine("=== Testing abstraction ===");
        Console.WriteLine($"{baseType} cannot be instantiated: "
            + (baseType.IsAbstract && !baseType.IsInterface));

        string propertyName = "TotalPrice";
        Console.WriteLine($"Property {propertyName} has no implementation: "
            + baseType.GetProperty(propertyName,
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.Static
            )?.GetGetMethod()?.IsAbstract);
        string methodName = "PrintInstructions";
        Console.WriteLine($"Method {methodName} has no implementation: "
            + baseType.GetMethod(methodName,
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance | BindingFlags.Static
            )?.IsAbstract);

        Console.WriteLine("\n=== Testing inheritance ===");
        Type derivedType = typeof(Electronics);
        bool isDerivedFromBase = derivedType.IsSubclassOf(baseType);
        Console.WriteLine($"Is {derivedType} derived from {baseType}? {isDerivedFromBase}");

        MethodInfo toStringMethod = baseType.GetMethod("ToString")!;
        Console.WriteLine($"Has {baseType.Name} overridden {toStringMethod.Name}? " +
            $"{toStringMethod.DeclaringType != typeof(object)}");

        propertyName = "TotalPrice";
        PropertyInfo? totalPriceProperty = baseType.GetProperty(propertyName);
        if (totalPriceProperty != null)
        {
            MethodInfo? getMethod = totalPriceProperty.GetMethod;
            if (getMethod != null)
            {
                Console.WriteLine($"{propertyName} is overridden: {getMethod.IsVirtual && !getMethod.IsFinal}");
            }
            else
            {
                Console.WriteLine($"No getter method found for {propertyName}.");
            }
        }
        else
        {
            Console.WriteLine($"Property {propertyName} not found.");
        }
    }

    public static void TestEncapsulation()
    {
    // Product
        string className = "Product";
        Console.WriteLine($"Class {className}");

        string propertyName = "TotalPrice";
        Console.WriteLine($" - Property {propertyName} encapsulation: "
            + TestAccessModifierProperty(className, propertyName, "Public", null));

    // Electronics
        className = "Electronics";
        Console.WriteLine($"\nClass {className}");

        string fieldName = "_instructions";
        Console.WriteLine($" - Field {fieldName} encapsulation: "
            + TestAccessModifierField(className, fieldName, "Private"));

    // CreditCardPayment
        className = "CreditCardPayment";
        Console.WriteLine($"\nClass {className}");

        propertyName = "Amount";
        Console.WriteLine($" - Property {propertyName} encapsulation: "
            + TestAccessModifierProperty(className, propertyName, "Public", "Public"));

        propertyName = "IsProcessed";
        Console.WriteLine($" - Property {propertyName} encapsulation: "
            + TestAccessModifierProperty(className, propertyName, "Public", "Private"));

        propertyName = "HolderName";
        Console.WriteLine($" - Property {propertyName} encapsulation: "
            + TestAccessModifierProperty(className, propertyName, "Public", null));
    
    // PaymentProcessor
        className = "PaymentProcessor";
        Console.WriteLine($"\nClass {className}");
        
        string methodName = "PrintOutcome";
        Console.WriteLine($" - Method {methodName} encapsulation: "
            + TestAccessModifierMethod(className, methodName, "Private"));
    }

    public static void TestFunctProductElectronics()
    {
        List<Electronics> electronics = [
            new Electronics("Computer", 1000, "Turn it on"),
            new Electronics("Keyboard", 50, "Plug & Play"),
        ];

        foreach (Electronics e in electronics)
        {
            Console.WriteLine(e);
            e.PrintInstructions();
            Console.WriteLine();
        }
    }

    public static void TestFunctCreditCardPayment()
    {
        List<CreditCardPayment> payments = [
            new CreditCardPayment(1000, "Jill Doe", "ABCD1234"),
            new CreditCardPayment(1250, "Jack Doe", "EFGH5678"),
        ];

        foreach (CreditCardPayment payment in payments)
        {
            Console.WriteLine($"Payment processed? {payment.IsProcessed}");
            payment.ProcessPayment();
            Console.WriteLine($"Payment processed? {payment.IsProcessed}\n");
        }
    }

    public static void TestPaymentProcessor()
    {
    // Successful CreditCardPayment
        Electronics product1 = new("Laptop", 1000, "Turn it on");
        CreditCardPayment payment1 = new(1200, "Alice", "ABCD123456"); // +20% tax
        Console.WriteLine("Successful CreditCardPayment");
        PaymentProcessor.ProcessPayment(product1, payment1);
        Console.WriteLine();

    // Successful CreditCardPayment
        Electronics product2 = new("Smartphone", 200, "Charge it");
        CreditCardPayment payment2 = new(240, "Bob", "XYZ987654");
        Console.WriteLine("Successful CreditCardPayment");
        PaymentProcessor.ProcessPayment(product2, payment2);
        Console.WriteLine();

    // Excessive payment for CreditCardPayment
        CreditCardPayment payment3 = new(400, "Eve", "NOP789012"); // Excessive amount
        Console.WriteLine("Excessive payment for CreditCardPayment");
        PaymentProcessor.ProcessPayment(product2, payment3);
        Console.WriteLine();

    // Payment with negative amount
        CreditCardPayment payment4 = new(-50, "Ivy", "XYZ876543");
        Console.WriteLine("Payment with negative amount");
        PaymentProcessor.ProcessPayment(product2, payment4);
        Console.WriteLine();

    // Payment processed twice
        CreditCardPayment payment5 = new(130, "Jack", "ABC987654");
        Console.WriteLine("Payment processed twice");
        PaymentProcessor.ProcessPayment(product2, payment5);
        PaymentProcessor.ProcessPayment(product2, payment5); // Attempt to process again
        Console.WriteLine();

    // Unsupported payment
        IDealPayment payment6 = new(130, "Killian", "DEF876543");
        Console.WriteLine("Unsupported payment");
        PaymentProcessor.ProcessPayment(product2, payment6!); // Unsupported
        Console.WriteLine();
    }

    private static void PrintIsFieldReadOnly(Type clsType, string fieldName)
    {
        FieldInfo? info = clsType.GetField(fieldName,
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.Static);

        if (info is null)
        {
            Console.WriteLine($"Field {fieldName} not found in {clsType.Name}");
            return;
        }

        Console.WriteLine($"{info.Name} is a read-only field: " +
            info.IsInitOnly);
    }

    private static void PrintIsFieldConstant(Type clsType, string fieldName)
    {
        FieldInfo? info = clsType.GetField(fieldName,
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.Static);

        if (info is null)
        {
            Console.WriteLine($"Field {fieldName} not found in {clsType.Name}");
            return;
        }

        Console.WriteLine($"{info.Name} is a constant field: " +
            info.IsLiteral);
    }

    private static bool CheckMethodSignature(Type interfaceType, string methodName, Type expectedReturnType, params Type[] expectedParameterTypes)
    {
        MethodInfo? method = interfaceType.GetMethod(methodName);

        if (method == null)
        {
            Console.WriteLine($" - Method {methodName} not found in the interface.");
            return false;
        }

        if (method.ReturnType != expectedReturnType)
        {
            Console.WriteLine($" - Incorrect return type for method {methodName}. Expected: {expectedReturnType}, Actual: {method.ReturnType}");
            return false;
        }

        var parameters = method.GetParameters();
        if (parameters.Length != expectedParameterTypes.Length)
        {
            Console.WriteLine($" - Incorrect number of parameters for method {methodName}. Expected: {expectedParameterTypes.Length}, Actual: {parameters.Length}");
            return false;
        }

        for (int i = 0; i < expectedParameterTypes.Length; i++)
        {
            if (parameters[i].ParameterType != expectedParameterTypes[i])
            {
                Console.WriteLine($" - Incorrect parameter type for parameter {parameters[i].Name} in method {methodName}. Expected: {expectedParameterTypes[i]}, Actual: {parameters[i].ParameterType}");
                return false;
            }
        }

        return true;
    }

    private static bool CheckPropertySignature(Type interfaceType, string propertyName, Type expectedPropertyType,
        bool canRead, bool canWrite)
    {
        PropertyInfo? property = interfaceType.GetProperty(propertyName);

        if (property == null)
        {
            Console.WriteLine($" - Property {propertyName} not found in the interface.");
            return false;
        }

        if (property.PropertyType != expectedPropertyType)
        {
            Console.WriteLine($" - Incorrect property type for {propertyName}. Expected: {expectedPropertyType}, Actual: {property.PropertyType}");
            return false;
        }

        return canRead == property.CanRead && canWrite == property.CanWrite;
    }

    private static string TestAccessModifierField(string cls, string field, string modifier)
    {
        FieldInfo? fieldInfo = Type.GetType(cls)?.GetField(field,
            BindingFlags.Public |  BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.Static);
        
        if (fieldInfo == null)
        {
            return $"Field not found: {field}";
        }

        bool flag = modifier switch
        {
            "Public" => fieldInfo.IsPublic,
            "Family" => fieldInfo.IsFamily,
            "Private" => fieldInfo.IsPrivate,
            _ => false,
        };

        return flag ? "Correct!" : "Incorrect.";
    }

    private static string TestAccessModifierProperty(string cls, string property, string? getTest, string? setTest)
    {
        PropertyInfo? propertyInfo = Type.GetType(cls)?.GetProperty(property,
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.Static);
        
        if (propertyInfo == null)
        {
            return $"Property not found: {property}";
        }

        bool flag = true; // Safe to initialize to true, because there is always at least a get or a set
        
        if (getTest != null)
        {
            MethodInfo getInfo = propertyInfo.GetMethod!;
            flag = flag && propertyInfo.CanRead && getTest switch
            {
                "Public" => getInfo.IsPublic,
                "Family" => getInfo.IsFamily,
                "Private" => getInfo.IsPrivate,
                _ => false
            };
        }

        if (setTest != null)
        {
            MethodInfo setInfo = propertyInfo.SetMethod!;
            flag = flag && propertyInfo.CanWrite && setTest switch
            {
                "Public" => setInfo.IsPublic,
                "Family" => setInfo.IsFamily,
                "Private" => setInfo.IsPrivate,
                _ => false
            };
        }

        return flag ? "Correct!" : "Incorrect.";
    }
    
    private static string TestAccessModifierMethod(string? cls, string? method, string? modifier)
    {
        var targetType = Type.GetType(cls);
        MethodInfo? methodInfo = Type.GetType(cls)?.GetMethod(method,
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.Static);

        if (methodInfo == null)
            return $"Method not found: {method}";

        bool flag = modifier switch
        {
            "Public" => methodInfo.IsPublic,
            "Family" => methodInfo.IsFamily,
            "Private" => methodInfo.IsPrivate,
            _ => false,
        };

        return flag ? "Correct!" : "Incorrect.";
    }
}
