using System;

namespace MineSweeper
{
    public class Board
    {
        public TileType[,] MineField { get; private set; }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public int MinePercentage { get; private set; }
        public int AmountOfMines { get; private set; }

        public TileType this[int line, int column]
        {
            get => MineField[line, column];
            set => MineField[line, column] = value;
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
            for (var line = 0; line < Width; line++)
            {
                for (var column = 0; column < Height; column++)
                {
                    if (MineField[line, column] == TileType.Mine)
                    {
                        numberOfMines++;
                    }
                }
            }

            return numberOfMines;
        }

        public Board Clone()
        {
            return new(MineField.Clone() as TileType[,])
            {
                Width = Width,
                Height = Height,
                MinePercentage = MinePercentage,
                AmountOfMines = AmountOfMines
            };
        }

        public void ToggleFlag(int line, int column)
        {
            if (MineField[line, column] == TileType.Flag)
                MineField[line, column] = TileType.Hidden;
            else if (MineField[line, column] == TileType.Hidden)
                MineField[line, column] = TileType.Flag;
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

            for (var line = 0; line < Width; line++)
            {
                for (var column = 0; column < Height; column++)
                {
                    MineField[line, column] = TileType.Hidden;
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

            for (var line = 0; line < Width; line++)
            {
                for (var column = 0; column < Height; column++)
                {
                    if (MineField[line, column] == TileType.Mine)
                    {
                        InsertMine(line, column);
                    }
                }
            }
        }

        public void ApplyBoardAnnotated()
        {
            Width = MineField.GetLength(0);
            Height = MineField.GetLength(1);
            AmountOfMines = 0;

            AmountOfMines = GetAmountOfMines();
            MinePercentage = AmountOfMines * 100 / (Width * Height);

            ValidateBoard();
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
                int line, column;
                do
                {
                    line = rand.Next(0, Width);
                    column = rand.Next(0, Height);
                } while (MineField[line, column] == TileType.Mine);

                InsertMine(line, column);
            }
        }

        private void InsertMine(int inputLine, int inputColumn)
        {
            MineField[inputLine, inputColumn] = TileType.Mine;
            for (var line = inputLine - 1; line <= inputLine + 1; line++)
            {
                if (line < 0 || line >= Width)
                    continue;
                for (var column = inputColumn - 1; column <= inputColumn + 1; column++)
                {
                    if (column < 0 || column >= Height)
                        continue;
                    if (MineField[line, column] != TileType.Mine)
                        MineField[line, column] += 1;
                }
            }
        }
    }
}