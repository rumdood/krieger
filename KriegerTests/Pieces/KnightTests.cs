using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krieger;
using NUnit.Framework;
namespace Krieger.Pieces.Tests
{
    [TestFixture()]
    public class KnightTests
    {
        private Board _board;
        private Knight _piece;
        private BoardCoordinate _startingLocation;

        [SetUp]
        public void Setup()
        {
            _board = new Board(8);
            _piece = new Knight(PlayerColor.White);
            _startingLocation = new BoardCoordinate(2, 1);
        }

        [TestFixture]
        public class GetMoves : KnightTests
        {
            [Test(), Category("Proven")]
            public void Returns_3_3_When_Passed_2_1()
            {
                var legalMoves = _piece.GetLegalMovesFromCoordinate(_startingLocation, _board.BoardSize);
                var target = new BoardCoordinate(3, 3);
                Assert.Contains(target, legalMoves.ToList());
            }

            [Test(), Category("Proven")]
            public void Returns_1_3_When_Passed_2_1()
            {
                var legalMoves = _piece.GetLegalMovesFromCoordinate(_startingLocation, _board.BoardSize);
                var target = new BoardCoordinate(1, 3);
                Assert.Contains(target, legalMoves.ToList());
            }

            [Test(), Category("Proven")]
            public void Does_Not_Return_Invalid_Squares_When_Passed_2_1()
            {
                var legalMoves = _piece.GetLegalMovesFromCoordinate(_startingLocation, _board.BoardSize);
                var illegalCount = legalMoves.Where(bc => !bc.IsValidForBoard(_board.BoardSize)).Count();
                Assert.AreEqual(0, illegalCount);
            }
        }
    }
}
