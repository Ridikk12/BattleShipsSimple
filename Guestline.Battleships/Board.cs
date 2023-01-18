namespace Guestline.Battleships;

public class Board : IBoard
{
    public int[,] _board { get; }

    public Board(int[,] board)
    {
        _board = board;
    }

    private (int row, int column) GetInitialPosition()
    {
        var rnd = new Random();

        return (rnd.Next(0, 10), rnd.Next(0, 10));
    }

    public void ResetBoard()
    {
        for (var row = 0; row < _board.GetLength(0); row++)
        {
            for (var column = 0; column < _board.GetLength(1); column++)
            {
                _board[row, column] = (int)BoardValue.Empty;
            }
        }
    }

    public void PlaceShip(ShipType shipType)
    {
        var numberOfTilesToPlace = (int)shipType;

        var initialPosition = GetInitialPosition();
        
        while (numberOfTilesToPlace > 0)
        {
            if (CanPlaceTile(initialPosition.row + 1, initialPosition.column))
            {
                _board[initialPosition.row + 1, initialPosition.column] = 1;
                initialPosition.row += 1;
                numberOfTilesToPlace--;
            }
            else if (CanPlaceTile(initialPosition.row - 1, initialPosition.column))
            {
                _board[initialPosition.row - 1, initialPosition.column] = 1;
                initialPosition.row -= 1;
                numberOfTilesToPlace--;
            }
            else if (CanPlaceTile(initialPosition.row, initialPosition.column + 1))
            {
                _board[initialPosition.row, initialPosition.column + 1] = 1;
                initialPosition.column += 1;
                numberOfTilesToPlace--;
            }
            else if (CanPlaceTile(initialPosition.row + 1, initialPosition.column - 1))
            {
                _board[initialPosition.row, initialPosition.column - 1] = 1;
                initialPosition.column -= 1;
                numberOfTilesToPlace--;
            }
            else
            {
                //Add logic for rollback in case of no possible placement.
                //Add logic to make it more random
                break;
            }
        }
    }

    public void Set(int row, int col, BoardValue value)
    {
        _board[row, col] = (int)value;
    }
    
    public bool CanPlaceTile(int row, int column)
    {
        var isTileOnBoard = IsTileOnBoard(row, column);
        if (!isTileOnBoard)
            return false;

        return _board[row, column] == (int)BoardValue.Empty;
    }

    bool IsTileOnBoard(int row, int column)
    {
        return row >= 0 && _board.GetLength(0) > row && column >= 0 && _board.GetLength(1) > column;
    }

    public bool CheckIfValueExistsOnBoard(BoardValue value)
    {
        var exists = false;
        for (var i = 0; i < _board.GetLength(0); i++)
        {
            for (var j = 0; j < _board.GetLength(1); j++)
            {
                if (_board[i, j] == (int)value)
                    exists = true;
            }
        }
        return exists;
    }
}