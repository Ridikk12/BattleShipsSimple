namespace Guestline.Battleships;

public interface IBoard
{
    void ResetBoard();
    void PlaceShip(ShipType shipType);
    void Set(int row, int col, BoardValue value);
    bool CanPlaceTile(int row, int column);
    bool CheckIfValueExistsOnBoard(BoardValue value);
}