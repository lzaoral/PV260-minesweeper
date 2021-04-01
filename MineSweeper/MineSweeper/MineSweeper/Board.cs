using System;
using System.Collections.Generic;

namespace MineSweeper
{
    public class Board
    {
        private TileType[,] _mineField;

        public int Width { get; private set; }
        public int Height { get; private set; }
        private int MinePercentage { get; set; }
        public int AmountOfMines { get; private set; }

        public TileType this[int x, int y]
        {
            get => _mineField[x, y];
            private set => _mineField[x, y] = value;
        }

        public Board(int width, int height, int minePercentage)
        {
            MinePercentage = minePercentage;
            Width = width;
            Height = height;
        }

        public Board(TileType[,] board)
        {
            _mineField = board;
        }

        private int GetAmountOfMines()
        {
            var numberOfMines = 0;
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    if (_mineField[x, y] == TileType.Mine)
                    {
                        numberOfMines++;
                    }
                }
            }

            return numberOfMines;
        }

        public Board Clone()
        {
            return new Board(_mineField.Clone() as TileType[,]);
        }

        public void ToggleFlag(int x, int y)
        {
            if (_mineField[x, y] == TileType.Flag)
                _mineField[x, y] = TileType.Hidden;
            else if (_mineField[x, y] == TileType.Hidden)
                _mineField[x, y] = TileType.Flag;
        }

        public void Generate()
        {
            ValidateBoard();

            AmountOfMines = Width * Height * MinePercentage / 100;
            // by default Enum is initialized to 0
            _mineField = new TileType[Width, Height];

            GenerateMineField();
        }

        public void GenerateHidden()
        {
            ValidateBoard();
            // by default Enum is initialized to 0
            _mineField = new TileType[Width, Height];

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    _mineField[x, y] = TileType.Hidden;
                }
            }
        }


        public void ApplyBoard()
        {
            Width = _mineField.GetLength(0);
            Height = _mineField.GetLength(1);
            AmountOfMines = 0;

            AmountOfMines = GetAmountOfMines();
            MinePercentage = AmountOfMines * 100 / (Width * Height);

            ValidateBoard();

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    if (_mineField[x, y] == TileType.Mine)
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
                } while (_mineField[posX, posY] == TileType.Mine);

                InsertMine(posX, posY);
            }
        }

        private void InsertMine(int posX, int posY)
        {
            _mineField[posX, posY] = TileType.Mine;
            for (var x = posX - 1; x <= posX + 1; x++)
            {
                if (x < 0 || x >= Width)
                    continue;
                for (var y = posY - 1; y <= posY + 1; y++)
                {
                    if (y < 0 || y >= Height)
                        continue;
                    if (_mineField[x, y] != TileType.Mine)
                        _mineField[x, y] += 1;
                }
            }
        }

        protected bool Equals(Board other)
        {
            return Equals(_mineField, other._mineField);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Board) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_mineField);
        }
    }
}