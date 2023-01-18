using Guestline.Battleships;

var gameBoard = new int [10, 10];

var shipsToPlace = new[]
{
    ShipType.Battleship,
    ShipType.Destroyer,
    ShipType.Destroyer
};

var columnsMapping = new Dictionary<char, int>()
{
    { 'A', 0 },
    { 'B', 1 },
    { 'C', 2 },
    { 'D', 3 },
    { 'E', 4 },
    { 'F', 5 },
    { 'G', 6 },
    { 'H', 7 },
    { 'I', 8 },
    { 'J', 9 }
};


var gameManager = new GameManager(shipsToPlace, new Board(gameBoard), columnsMapping);
gameManager.Run();