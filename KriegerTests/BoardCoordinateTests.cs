using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Krieger;
using NUnit.Framework;

namespace Krieger.Tests
{
    [TestFixture]
    class BoardCoordinateTests
    {
        [Test, Category("IsValidForBoard")]
        public void Returns_False_for_X_Less_Than_1()
        {
            var boardCoordinate = new BoardCoordinate(0, 8);
            Assert.IsFalse(boardCoordinate.IsValidForBoard(8));
        }

        [Test, Category("IsValidForBoard")]
        public void Returns_False_for_Y_Less_Than_1()
        {
            var boardCoordinate = new BoardCoordinate(8, 0);
            Assert.IsFalse(boardCoordinate.IsValidForBoard(8));
        }

        [Test, Category("IsValidForBoard")]
        public void Returns_False_for_X_Greater_Than_BoardSize()
        {
            var boardCoordinate = new BoardCoordinate(9, 8);
            Assert.IsFalse(boardCoordinate.IsValidForBoard(8));
        }

        [Test, Category("IsValidForBoard")]
        public void Returns_False_for_Y_Great_Than_BoardSize()
        {
            var boardCoordinate = new BoardCoordinate(8, 9);
            Assert.IsFalse(boardCoordinate.IsValidForBoard(8));
        }

        [Test, Category("IsValidForBoard")]
        public void Returns_True_for_Valid_Coordinate()
        {
            var boardCoordinate = new BoardCoordinate(1, 8);
            Assert.IsTrue(boardCoordinate.IsValidForBoard(8));
        }
    }
}
