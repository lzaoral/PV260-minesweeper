namespace MineSweeper
{
    public class Game
    {
        private readonly Board _board;
        private readonly Board _referenceBoard;

        public TileType this[int x, int y] => _board[x, y];

        public Game(int width, int height, int minePercentage)
        {
            _board = new Board(width, height, minePercentage);
            _referenceBoard = new Board(width, height, minePercentage);

            _referenceBoard.Generate();
            _board.GenerateHidden();
        }

        public Board ToggleFlag(int x, int y)
        {
            _board.ToggleFlag(x, y);
            return _board.Clone();
        }
    }
}