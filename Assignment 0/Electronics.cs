class Electronics : Product
{
    public double TaxRate;
    public double TotalPrice;
    public string _instructions;

    public Electronics(string name, double basePrice, string instructions)
    {

    }

    public void PrintInstructions() => Console.WriteLine($"{Name}: {_instructions}");
}
