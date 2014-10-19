using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krieger.Pieces;
using NUnit.Framework;
namespace Krieger.Pieces.Tests
{
    [TestFixture()]
    public class KingTests
    {
        private Board _board;
        private Piece _piece;
        private BoardCoordinate _startingLocation;

        [SetUp]
        public void Setup()
        {
            _board = new Board(8);
            _piece = new King(PlayerColor.White);
            _startingLocation = new BoardCoordinate(1, 1);
        }

        [TestFixture]
        public class Get_Moves : KingTests
        {
            [Test(), Category("GetLegalMoves")]
            public void Returns_1_2_When_Passed_1_1()
            {
                var legalMoves = _piece.GetLegalMovesFromCoordinate(_startingLocation, _board.BoardSize);
                var target = new BoardCoordinate(1, 2);
                Assert.IsTrue(legalMoves.Contains(target));
            }

            [Test(), Category("GetLegalMoves")]
            public void Returns_1_1_When_Passed_1_2()
            {
                var legalMoves = _piece.GetLegalMovesFromCoordinate(new BoardCoordinate(1, 2), _board.BoardSize);
                var target = new BoardCoordinate(1, 1);
                Assert.IsTrue(legalMoves.Contains(target));
            }

            [Test(), Category("GetLegalMoves")]
            public void Returns_2_1_When_Passed_1_1()
            {
                var legalMoves = _piece.GetLegalMovesFromCoordinate(_startingLocation, _board.BoardSize);
                var target = new BoardCoordinate(2, 1);
                Assert.IsTrue(legalMoves.Contains(target));
            }

            [Test(), Category("GetLegalMoves")]
            public void Returns_2_2_When_Passed_1_1()
            {
                var legalMoves = _piece.GetLegalMovesFromCoordinate(_startingLocation, _board.BoardSize);
                var target = new BoardCoordinate(2, 2);
                Assert.IsTrue(legalMoves.Contains(target));
            }

            [Test(), Category("GetLegalMoves")]
            public void Does_Not_Return_1_3_When_Passed_1_1()
            {
                var legalMoves = _piece.GetLegalMovesFromCoordinate(_startingLocation, _board.BoardSize);
                var target = new BoardCoordinate(1, 3);
                Assert.IsFalse(legalMoves.Contains(target));
            }

            [Test(), Category("GetLegalMoves")]
            public void Does_Not_Return_Invalid_Coordinate()
            {
                var legalMoves = _piece.GetLegalMovesFromCoordinate(_startingLocation, _board.BoardSize);
                var illegalMoves = legalMoves.Where(x => !x.IsValidForBoard(_board.BoardSize));
                Assert.IsTrue(illegalMoves.Count() == 0);
            }
        }
    }
}
