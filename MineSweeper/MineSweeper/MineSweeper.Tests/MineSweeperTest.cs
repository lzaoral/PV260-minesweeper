using System;
using NUnit.Framework;

namespace MineSweeper.Tests
{
    public class Tests
    {
        [Test]
        [TestCase(1, 1, 1)]
        [TestCase(2, 2, 30)]
        [TestCase(3, 3, 19)]
        [TestCase(10, 10, 40)]
        [TestCase(51, 10, 25)]
        [TestCase(10, 51, 25)]
        [TestCase(10, 10, 69)]
        public void ValidateGameBoardDimensions(int width, int height, int minePercentage)
        {
            var gameBoard = new Board(width, height, minePercentage);
            if (width < 3 || height < 3 || minePercentage < 20 || width > 50 || height > 50 || minePercentage > 60)
            {
                Assert.Throws<ArgumentException>(gameBoard.Generate);
            }
            else
            {
                Assert.DoesNotThrow(gameBoard.Generate);
                Assert.AreEqual(width, gameBoard.Width);
                Assert.AreEqual(height, gameBoard.Height);
                Assert.AreEqual((width * height * minePercentage / 100), gameBoard.AmountOfMines);
            }
        }
    }
}