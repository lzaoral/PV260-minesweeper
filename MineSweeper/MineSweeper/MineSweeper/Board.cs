using System;

namespace MineSweeper
{
    public class Board
    {
        public TileType[,] MineField { get; private set; }

        public int Width { get; private set; }
        public int Height { get; private set; }
        private int MinePercentage { get; set; }
        public int AmountOfMines { get; private set; }

        public TileType this[int x, int y]
        {
            get => MineField[x, y];
            private set => MineField[x, y] = value;
        }

        public Board(int width, int height, int minePercentage)
        {
            MinePercentage = minePercentage;
            Width = width;
            Height = height;
        }

        public Board(TileType[,] board)
        {
            MineField = board;
        }

        private int GetAmountOfMines()
        {
            var numberOfMines = 0;
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    if (MineField[x, y] == TileType.Mine)
                    {
                        numberOfMines++;
                    }
                }
            }

            return numberOfMines;
        }

        public Board Clone()
        {
            return new Board(MineField.Clone() as TileType[,]);
        }

        public void ToggleFlag(int x, int y)
        {
            if (MineField[x, y] == TileType.Flag)
                MineField[x, y] = TileType.Hidden;
            else if (MineField[x, y] == TileType.Hidden)
                MineField[x, y] = TileType.Flag;
        }

        public void Generate()
        {
            ValidateBoard();

            AmountOfMines = Width * Height * MinePercentage / 100;
            // by default Enum is initialized to 0
            MineField = new TileType[Width, Height];

            GenerateMineField();
        }

        public void GenerateHidden()
        {
            ValidateBoard();
            // by default Enum is initialized to 0
            MineField = new TileType[Width, Height];

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    MineField[x, y] = TileType.Hidden;
                }
            }
        }


        public void ApplyBoard()
        {
            Width = MineField.GetLength(0);
            Height = MineField.GetLength(1);
            AmountOfMines = 0;

            AmountOfMines = GetAmountOfMines();
            MinePercentage = AmountOfMines * 100 / (Width * Height);

            ValidateBoard();

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    if (MineField[x, y] == TileType.Mine)
                    {
                        InsertMine(x, y);
                    }
                }
            }
        }

        private void ValidateBoard()
        {
            if (Width < 3 || Width > 50 ||
                Height < 3 || Height > 50 ||
                MinePercentage < 20 || MinePercentage > 60)
            {
                throw new ArgumentException("Board is not valid");
            }
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
                } while (MineField[posX, posY] == TileType.Mine);

                InsertMine(posX, posY);
            }
        }

        private void InsertMine(int posX, int posY)
        {
            MineField[posX, posY] = TileType.Mine;
            for (var x = posX - 1; x <= posX + 1; x++)
            {
                if (x < 0 || x >= Width)
                    continue;
                for (var y = posY - 1; y <= posY + 1; y++)
                {
                    if (y < 0 || y >= Height)
                        continue;
                    if (MineField[x, y] != TileType.Mine)
                        MineField[x, y] += 1;
                }
            }
        }
    }
}