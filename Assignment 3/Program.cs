static class Program
{
    static void Main(string[] args)
    {
        switch (args[1])
        {
            case "Reset": TestLambdaReset(); return;
            case "PrintTurnPlayer": TestLambdaPrintTurnPlayer(); return;
            case "Remove": TestRemove(); return;
            default: throw new ArgumentOutOfRangeException($"{args[1]}", $"Unexpected args value: {args[1]}");
        }
    }
    
    public static void TestLambdaReset()
    {
        Go go = new();

        Console.WriteLine("=== Game 1 ===");
        for (int i = 0; i < Go.BoardSize; i++)
        {
            go.PlacePiece(i, i);
        }
        go.PrintBoard();

        Console.WriteLine("Resetting game...");
        GoUtils.GetAction("Reset")(go);
        go.PrintBoard();
        Console.WriteLine($"Turn player: {go.TurnPlayer}");

        Console.WriteLine("\n=== Game 2 ===");
        for (int i = 0; i < 6; i++)
        {
            go.PlacePiece(0, i);
        }
        go.PrintBoard();

        Console.WriteLine("Resetting game...");
        GoUtils.GetAction("Reset")(go);
        go.PrintBoard();
        Console.WriteLine($"Turn player: {go.TurnPlayer}");
    }

    public static void TestLambdaPrintTurnPlayer()
    {
        Go go = new();
        GoUtils.GetAction("PrintTurnPlayer")(go); // Black
        
        go.PlacePiece(0, 0);
        GoUtils.GetAction("PrintTurnPlayer")(go); // White
        
        go.PlacePiece(0, 1);
        GoUtils.GetAction("PrintTurnPlayer")(go); // Black
        
        go.PlacePiece(0, 1);
        GoUtils.GetAction("PrintTurnPlayer")(go); // Invalid move; still Black's turn
    }

    public static void TestRemove()
    {
        RemoveScenario1();
        RemoveScenario2();
        RemoveScenario3();
        RemoveScenario4();
        RemoveScenario5();
    }

    private static void RemoveScenario1()
    {
        Console.WriteLine("=== Scenario 1 ===");
        char[,] board = new char[Go.BoardSize, Go.BoardSize]
        {
            { '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', 'B', '.', '.', '.', '.' },
            { '.', '.', 'B', 'W', 'B', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.' },
        };

        Go goGame = new(board, 'B'); // Black to move
        goGame.PrintBoard();

        int x = 5;
        int y = 3;
        Console.WriteLine($"\nBlack places a piece at ({x}, {y}) and captures White");
        goGame.PlacePiece(x, y);

        Console.WriteLine("\nBoard after capturing:");
        goGame.PrintBoard();
    }
    private static void RemoveScenario2()
    {
        Console.WriteLine("\n=== Scenario 2 ===");
        char[,] board = new char[Go.BoardSize, Go.BoardSize]
        {
            { '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', 'W', '.', '.', '.', '.', '.', '.' },
            { 'W', 'B', 'B', 'W', 'W', '.', '.', '.' },
            { '.', 'W', 'B', 'B', 'B', 'W', '.', '.' },
            { '.', 'W', 'B', 'W', 'B', 'W', '.', '.' },
            { '.', '.', 'W', '.', 'W', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.' },
        };

        Go goGame = new(board, 'W'); // White to move
        goGame.PrintBoard();

        int x = 1;
        int y = 2;
        Console.WriteLine($"\nWhite places a piece at ({x}, {y}) and captures Black");
        goGame.PlacePiece(x, y);

        Console.WriteLine("\nBoard after capturing:");
        goGame.PrintBoard();
    }
    private static void RemoveScenario3()
    {
        Console.WriteLine("\n=== Scenario 3 ===");
        char[,] board = new char[Go.BoardSize, Go.BoardSize]
        {
            { 'W', '.', 'B', '.', '.', '.', '.', '.' },
            { 'W', 'W', 'B', '.', '.', '.', '.', '.' },
            { 'B', 'B', 'B', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.' },
        };

        Go goGame = new(board, 'B'); // Black to move
        goGame.PrintBoard();

        int x = 0;
        int y = 1;
        Console.WriteLine($"\nBlack places a piece at ({x}, {y}) and captures White");
        goGame.PlacePiece(x, y);

        Console.WriteLine("\nBoard after Black captures White:");
        goGame.PrintBoard();
    }
    private static void RemoveScenario4()
    {
        Console.WriteLine("\n=== Scenario 4 ===");
        char[,] board = new char[Go.BoardSize, Go.BoardSize]
        {
            { 'W', 'B', 'B', 'B', 'W', '.', '.', '.' },
            { 'W', 'W', '.', 'B', 'B', 'W', '.', '.' },
            { 'W', 'W', 'W', 'B', 'W', '.', '.', '.' },
            { '.', 'W', 'B', 'B', 'B', 'W', '.', '.' },
            { '.', 'W', 'B', 'B', 'B', 'W', '.', '.' },
            { '.', '.', 'W', 'B', 'W', '.', '.', '.' },
            { '.', '.', 'W', 'B', 'B', 'W', '.', '.' },
            { '.', '.', 'W', 'B', 'W', '.', '.', '.' },
        };

        Go goGame = new(board, 'W'); // White to move
        goGame.PrintBoard();

        int x = 1;
        int y = 2;
        Console.WriteLine($"\nWhite places a piece at ({x}, {y}) and captures Black");
        goGame.PlacePiece(x, y);

        Console.WriteLine("\nBoard after capturing:");
        goGame.PrintBoard();
    }
    private static void RemoveScenario5()
    {
        Console.WriteLine("\n=== Scenario 5 ===");
        char[,] board = new char[Go.BoardSize, Go.BoardSize]
        {
            { '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', 'B', '.', 'B', '.', '.' },
            { '.', 'B', 'B', 'W', 'B', 'W', '.', '.' },
            { 'W', 'B', 'W', '.', 'W', 'B', '.', '.' },
            { 'W', 'W', 'W', 'W', 'B', 'B', '.', '.' },
            { 'W', 'W', 'W', 'W', 'W', 'W', 'B', '.' },
        };

        Go goGame = new(board, 'B'); // Black to move
        goGame.PrintBoard();

        int x = 5;
        int y = 3;
        Console.WriteLine($"\nBlack places a piece at ({x}, {y}) and captures White");
        goGame.PlacePiece(x, y);

        Console.WriteLine("\nBoard after capturing:");
        goGame.PrintBoard();
    }
}
