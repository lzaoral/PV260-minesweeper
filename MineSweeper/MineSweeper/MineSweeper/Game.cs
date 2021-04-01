namespace MineSweeper
{
    public class Game
    {
        public Board Board => _board.Clone();
        private readonly Board _board;
        private readonly Board _referenceBoard;

        public TileType this[int line, int column] => _board[line, column];

        public Game(int width, int height, int minePercentage)
        {
            _board = new Board(width, height, minePercentage);
            _referenceBoard = new Board(width, height, minePercentage);

            _referenceBoard.Generate();
            _board.GenerateHidden();
        }

        public Game(TileType[,] inputField)
        {
            _referenceBoard = new Board(inputField);
            _referenceBoard.ApplyBoardAnnoted();

            _board = new Board(_referenceBoard.Width, _referenceBoard.Height, _referenceBoard.MinePercentage);
            _board.GenerateHidden();
        }

        public Board ToggleFlag(int line, int column)
        {
            _board.ToggleFlag(line, column);
            return _board.Clone();
        }

        private void UncoverTileInternal(int line, int column)
        {
            if (_board[line, column] != TileType.Hidden)
                return;
            _board[line, column] = _referenceBoard[line, column];

            // only continue to uncover if we're on an empty space
            if (_board[line, column] != TileType.Zero)
                return;

            for (var i = -1; i <= 1; i++)
            {
                for (var j = -1; j <= 1; j++)
                {
                    var line2 = line + i;
                    var column2 = column + j;
                    if (line2 < 0 || line2 >= _board.Width)
                        continue;
                    if (column2 < 0 || column2 >= _board.Height)
                        continue;
                    UncoverTileInternal(line2, column2);
                }
            }
        }

        public Board UncoverTile(int line, int column)
        {
            UncoverTileInternal(line, column);
            return _board.Clone();
        }
    }
}