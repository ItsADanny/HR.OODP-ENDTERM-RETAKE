class CopyingMachine : Machine
{
    public int PctInkCyan { get; private set; }
    public int PctInkMagenta { get; private set; }
    public int PctInkYellow { get; private set; }

    public CopyingMachine(string name, List<int> pctInk) // Change pctInk to type ValueTuple of ints named Cyan, Magenta and Yellow
        : base(name)
    {
        PctInkCyan = pctInk[0]; // Change to correctly get the value out of the ValueTuple
        PctInkMagenta = pctInk[1]; // Change to correctly get the value out of the ValueTuple
        PctInkYellow = pctInk[2]; // Change to correctly get the value out of the ValueTuple
    }

    public void Copy()
    {
        if (PctInkCyan == 0 || PctInkMagenta == 0 || PctInkYellow == 0)
        {
            AddIncident("Out of ink");
            return;
        }

        PctInkCyan--;
        PctInkMagenta--;
        PctInkYellow--;
    }

    public void GetPctInk() // Change the return type to a ValueTuple of ints named Cyan, Magenta and Yellow
    {
        return; // Return the ValueTuple
    }

    public override void AddIncident(string description)
    {
        base.AddIncident(
            $"{description}\n" +
            $"   - Cyan: {PctInkCyan}%\n" +
            $"   - Magenta: {PctInkMagenta}%\n" +
            $"   - Yellow: {PctInkYellow}%"
        );
    }
}
