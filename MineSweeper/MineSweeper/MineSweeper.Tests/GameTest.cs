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
        public void GivenCorrectCoordinatesToggleFlag(int x, int y)
        {
            var game = new Game(5, 5, 50);
            Assert.AreEqual(TileType.Hidden, game[x, y]);

            Assert.DoesNotThrow(() => game.ToggleFlag(x, y));
            Assert.AreEqual(TileType.Flag, game[x, y]);

            Assert.DoesNotThrow(() => game.ToggleFlag(x, y));
            Assert.AreEqual(TileType.Hidden, game[x, y]);
        }

        [Test]
        [TestCase(-5, 1)]
        [TestCase(1, 1000)]
        public void GivenIncorrectCoordinatesToggleFlag(int x, int y)
        {
            var game = new Game(5, 5, 50);
            Assert.Throws<IndexOutOfRangeException>(() => game.ToggleFlag(x, y));
        }
    }
}