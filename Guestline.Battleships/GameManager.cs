namespace Guestline.Battleships;

public class GameManager : IGameManager
{
    private readonly Board _board;
    private readonly ShipType[] _shipsToPlace;
    private readonly Dictionary<char, int> _columnsMapping;
    private const char ShipChar = 'S';
    private const char EmptyChar = '0';
    private const char HitChar = 'X';

    private const string InputValidationMessage =
        "Input is in wrong format. Specify column (A-J) and row (1-10). Example: A7";

    private const string HitMessage = "Hit!";
    private const string WinMessage = "You Win!";
    private const string MissMessage = "You Miss!";
    private const string InputMessage = "Where do you want to shot?!";

    private const string WelcomeMessage =
        "Try to shoot all computer' ships. Input coordinates in format [A-J][1-10]. Example A1";

    public GameManager(ShipType[] shipsToPlace, Board board, Dictionary<char, int> columnsMapping)
    {
        _shipsToPlace = shipsToPlace;
        _board = board;
        _columnsMapping = columnsMapping;
    }

    public void Run()
    {
        InitializeGame();

        Console.WriteLine(WelcomeMessage);

        while (true)
        {
            PrintBoard(PrintMode.Player);
            Console.WriteLine();
            var key = Console.ReadLine();

            if (!IsInputValid(key))
            {
                Console.WriteLine(InputValidationMessage);
                continue;
            }

            var position = ExtractPosition(key);

            if (CheckIfHit(position))
            {
                Console.WriteLine(HitMessage);
            }
            else
            {
                Console.WriteLine(MissMessage);
            }

            if (!_board.CheckIfValueExistsOnBoard(BoardValue.Ship))
            {
                PrintBoard(PrintMode.Full);
                Console.WriteLine(WinMessage);
                Console.ReadKey();
                break;
            }
        }
    }

    private void InitializeGame()
    {
        _board.ResetBoard();

        foreach (var shipType in _shipsToPlace)
        {
            _board.PlaceShip(shipType);
        }
    }
    
    private void PrintBoard(PrintMode mode)
    {
        var gameBoard = _board._board;
        var shipChar = mode == PrintMode.Player ? EmptyChar : ShipChar;
        for (var row = 0; row < gameBoard.GetLength(0); row++)
        {
            Console.WriteLine();
            for (var column = 0; column < gameBoard.GetLength(1); column++)
            {
                switch (gameBoard[row, column])
                {
                    case 0:
                        Console.Write($"{EmptyChar} ");
                        break;
                    case 1:
                        Console.Write($"{shipChar} ");
                        break;
                    case 2:
                        Console.Write($"{HitChar} ");
                        break;
                    default:
                        Console.Write($"{EmptyChar} ");
                        break;
                }
            }
        }
    }

    private bool IsInputValid(string key)
    {
        if (key.Length is not (2 or 3))
        {
            Console.WriteLine(InputValidationMessage);
            return false;
        }

        if (!_columnsMapping.ContainsKey(key[0]))
        {
            Console.WriteLine(InputValidationMessage);
            return false;
        }

        var row = GetNumberFromString(key);

        if (row is < 0 or > 10)
        {
            Console.WriteLine(InputValidationMessage);
            return false;
        }

        return true;
    }

    private (int row, int col) ExtractPosition(string key)
    {
        var row = GetNumberFromString(key);
        var col = _columnsMapping[key[0]];
        return (row, col);
    }

    private int GetNumberFromString(string s)
    {
        if (!s.Any())
            return -1;

        return Convert.ToInt16(new string(s.Where(char.IsDigit).ToArray()));
    }

    private bool CheckIfHit((int row, int col) position)
    {
        if (!_board.CanPlaceTile(position.row - 1, position.col))
        {
            _board.Set(position.row - 1, position.col, BoardValue.Hit);
            return true;
        }

        return false;
    }
}