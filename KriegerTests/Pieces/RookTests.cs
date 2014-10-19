using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krieger;
using NUnit.Framework;
namespace Krieger.Tests
{
    [TestFixture()]
    public class RookTests
    {
        private Board _board;
        private Rook _piece;
        private BoardCoordinate _startingLocation;

        [SetUp]
        public void Setup()
        {
            _board = new Board(8);
            _piece = new Rook(PlayerColor.White);
            _startingLocation = new BoardCoordinate(1, 1);
        }

        [TestFixture]
        public class Get_Moves : RookTests
        {
            [Test()]
            public void Returns_2_1_When_Passed_1_1()
            {
                var legalMoves = _piece.GetLegalMovesFromCoordinate(_startingLocation, _board.BoardSize);
                var target = new BoardCoordinate(2, 1);
                Assert.IsTrue(legalMoves.Contains(target));
            }

            [Test()]
            public void Returns_8_1_When_Passed_1_1()
            {
                var legalMoves = _piece.GetLegalMovesFromCoordinate(_startingLocation, _board.BoardSize);
                var target = new BoardCoordinate(8, 1);
                Assert.IsTrue(legalMoves.Contains(target));
            }

            [Test()]
            public void Returns_1_8_When_Passed_1_1()
            {
                var legalMoves = _piece.GetLegalMovesFromCoordinate(_startingLocation, _board.BoardSize);
                var target = new BoardCoordinate(1, 8);
                Assert.IsTrue(legalMoves.Contains(target));
            }

            [Test()]
            public void Does_Not_Return_Diagonal()
            {
                var legalMoves = _piece.GetLegalMovesFromCoordinate(_startingLocation, _board.BoardSize);
                var target = new BoardCoordinate(2, 2);
                Assert.IsFalse(legalMoves.Contains(target));
            }
        }
    }
}
