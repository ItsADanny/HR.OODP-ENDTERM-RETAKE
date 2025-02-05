class Go
{
    public const int BoardSize = 8;
    public readonly char[,] Board = new char[BoardSize, BoardSize];
    
    public const char Black = 'B';
    public const char White = 'W';
    public const char Empty = '.';
    
    public const char StartPlayer = Black; // Black is the first to move
    public char TurnPlayer { get; set; } = Black;
    public char Opponent => TurnPlayer == Black ? White : Black;

    public Go()
    {
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                Board[i, j] = Empty;
            }
        }
    }

    public Go(char[,] board, char turnPlayer)
    {
        Board = board;
        TurnPlayer = turnPlayer;
    }

    public void PlacePiece(int x, int y)
    {
        if (!IsValidPosition(x, y) || Board[x, y] != Empty)
        {
            Console.WriteLine("Invalid move.");
            return;
        }

        Board[x, y] = TurnPlayer;

        // Check if the move captures any opponent pieces
        CheckAndCapture();

        TurnPlayer = Opponent;
    }

    private static bool IsValidPosition(int x, int y)
    {
        return x >= 0 && x < BoardSize && y >= 0 && y < BoardSize;
    }

    // Check if the opponent's groups are surrounded and capture them
    private void CheckAndCapture()
    {
        bool[,] visited = new bool[BoardSize, BoardSize];

        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                if (Board[i, j] == Opponent && !visited[i, j])
                {
                    // If the group has no liberties, capture it
                    if (!HasLiberties(i, j, visited))
                    {
                        CaptureGroup(i, j);
                    }
                }
            }
        }
    }

    private bool HasLiberties(int x, int y, bool[,] visited)
    {
        if (!IsValidPosition(x, y)) { return false; }
        if (Board[x, y] == Empty) { return true; } // Found a liberty
        if (Board[x, y] != Opponent || visited[x, y]) { return false; } // Either it's not the opponent's stone or already visited

        visited[x, y] = true; // Mark this position as visited

        bool upLiberty = HasLiberties(x - 1, y, visited);
        bool downLiberty = HasLiberties(x + 1, y, visited);
        bool leftLiberty = HasLiberties(x, y - 1, visited);
        bool rightLiberty = HasLiberties(x, y + 1, visited);

        return upLiberty || downLiberty || leftLiberty || rightLiberty;
    }

    private void CaptureGroup(int x, int y)
    {
        
    }

    public void PrintBoard()
    {
        Console.Write("  ");
        for (int i = 0; i < BoardSize; i++)
        {
            Console.Write($"{i} ");
        }

        Console.Write("\n  -");
        for (int i = 0; i < BoardSize - 1; i++)
        {
            Console.Write("--");
        }
        Console.WriteLine();

        for (int i = 0; i < BoardSize; i++)
        {
            Console.Write($"{i}|");
            for (int j = 0; j < BoardSize; j++)
            {
                Console.Write($"{Board[i, j]} ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}
