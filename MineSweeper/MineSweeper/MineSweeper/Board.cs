using System;

namespace MineSweeper
{
    public class Board
    {
        private TileType[,] _board;

        public int Width { get; }
        public int Height { get; }
        private int MinePercentage { get; }
        public int AmountOfMines { get; private set; }

        public Board(int width, int height, int minePercentage)
        {
            MinePercentage = minePercentage;
            Width = width;
            Height = height;
        }

        public void Generate()
        {
            if (Width < 3 || Width > 50 ||
                Height < 3 || Height > 50 ||
                MinePercentage < 20 || MinePercentage > 60)
            {
                throw new ArgumentException("Board is not valid");
            }

            AmountOfMines = Width * Height * MinePercentage / 100;
            // by default Enum is initialized to 0
            _board = new TileType[Width, Height];

            GenerateMineField();
        }

        private void GenerateMineField()
        {
            var rand = new Random();
            for (var i = 0; i < AmountOfMines; i++)
            {
                int posX, posY;
                do
                {
                    posX = rand.Next(0, Width);
                    posY = rand.Next(0, Height);
                } while (_board[posX, posY] == TileType.Mine);

                InsertMine(posX, posY);
            }
        }

        private void InsertMine(int posX, int posY)
        {
            _board[posX, posY] = TileType.Mine;
            for (var x = posX - 1; x <= posX + 1; x++)
            {
                if (x < 0 || x >= Width)
                    continue;
                for (var y = posY - 1; y <= posY + 1; y++)
                {
                    if (y < 0 || y >= Height)
                        continue;
                    if (_board[x, y] != TileType.Mine)
                        _board[x, y] += 1;
                }
            }
        }
    }
}