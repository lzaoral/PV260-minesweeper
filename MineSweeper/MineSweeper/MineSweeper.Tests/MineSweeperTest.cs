using NUnit.Framework;

namespace MineSweeper.Tests
{
    public class Tests
    {
        [Test]
        [TestCase(1,1,1)]
        public void ValidateGameboardDimensions(int width, int height, int minePercentage)
        {
            if (width < 3 || height < 3 || minePercentage < 20 || width > 50 || height > 50 || minePercentage > 60)
            {
                Assert.Throws(MineSweeper.Board(width, height, minePercentage));
            }
            else
            {
                Assert.DoesNotThrow(MineSweeper.Board(width, height, minePercentage));
                var gameBoard = new MineSweeper.Board(width, height, minePercentage);
                Assert.AreEqual(width, gameBoard.getWidth());
                Assert.AreEqual(height, gameBoard.getHeight());
                Assert.AreEqual((width * height * minePercentage / 100), gameBoard.getNumberOfMines());
            }
        }
    }
}