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
    public class BishopTests
    {
        private Board _board;
        private BoardCoordinate _startingLocation;
        private Piece _piece;

        [SetUp]
        public void Setup()
        {
            _board = new Board(8);
            _startingLocation = new BoardCoordinate(1, 1);
            _piece = new Bishop(PlayerColor.White);
        }

        [TestFixture]
        public class Get_Moves : BishopTests
        {

            [Test(),Category("GetLegalMoves")]
            public void Returns_2_2_When_Passed_1_1()
            {
                var legalMoves = _piece.GetLegalMovesFromCoordinate(_startingLocation, _board.BoardSize);
                var target = new BoardCoordinate(2, 2);
                Assert.IsTrue(legalMoves.Contains(target));
            }

            [Test(), Category("GetLegalMoves")]
            public void Returns_8_8_When_Passed_1_1()
            {
                var legalMoves = _piece.GetLegalMovesFromCoordinate(_startingLocation, _board.BoardSize);
                var target = new BoardCoordinate(8, 8);
                Assert.IsTrue(legalMoves.Contains(target));
            }

            [Test(), Category("GetLegalMoves")]
            public void Returns_1_1_When_Passed_5_5()
            {
                var legalMoves = _piece.GetLegalMovesFromCoordinate(new BoardCoordinate(5, 5), _board.BoardSize);
                var target = new BoardCoordinate(1, 1);
                Assert.IsTrue(legalMoves.Contains(target));
            }

            [Test(), Category("GetLegalMoves")]
            public void Does_Not_Return_1_2_When_Passed_1_1()
            {
                var legalMoves = _piece.GetLegalMovesFromCoordinate(_startingLocation, _board.BoardSize);
                var target = new BoardCoordinate(1, 2);
                Assert.IsFalse(legalMoves.Contains(target));
            }
        }
    }
}
