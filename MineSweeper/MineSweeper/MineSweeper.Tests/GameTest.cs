using System;
using NUnit.Framework;

namespace MineSweeper.Tests
{
    public class GameTest
    {
        [Test]
        [TestCase(1, 1)]
        [TestCase(1, 3)]
        [TestCase(4, 2)]
        public void GivenCorrectCoordinatesToggleFlag(int line, int column)
        {
            var game = new Game(5, 5, 50);
            Assert.AreEqual(TileType.Hidden, game[line, column]);

            Assert.DoesNotThrow(() => game.ToggleFlag(line, column));
            Assert.AreEqual(TileType.Flag, game[line, column]);

            Assert.DoesNotThrow(() => game.ToggleFlag(line, column));
            Assert.AreEqual(TileType.Hidden, game[line, column]);
        }

        [Test]
        [TestCase(-5, 1)]
        [TestCase(1, 1000)]
        public void GivenIncorrectCoordinatesToggleFlag(int x, int y)
        {
            var game = new Game(5, 5, 50);
            Assert.Throws<IndexOutOfRangeException>(() => game.ToggleFlag(x, y));
        }

        [Test]
        public void ClickEmptySpace()
        {
            var inputField = new[,]
            {
                {TileType.Mine, TileType.Two, TileType.Mine, TileType.Mine},
                {TileType.One, TileType.Two, TileType.Three, TileType.Three},
                {TileType.Zero, TileType.Zero, TileType.One, TileType.Mine},
                {TileType.Zero, TileType.Zero, TileType.One, TileType.One},
            };
            var expected = new[,]
            {
                {TileType.Hidden, TileType.Hidden, TileType.Hidden, TileType.Hidden},
                {TileType.One, TileType.Two, TileType.Three, TileType.Hidden},
                {TileType.Zero, TileType.Zero, TileType.One, TileType.Hidden},
                {TileType.Zero, TileType.Zero, TileType.One, TileType.Hidden},
            };
            // input mine field here
            var game = new Game(inputField);
            var returnedBoard = game.UncoverTile(2, 1);
            Assert.AreEqual(expected, returnedBoard.MineField);
        }
    }
}