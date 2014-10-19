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
    public class QueenTests
    {
        private Board _board;
        private Queen _piece;
        private BoardCoordinate _startLocation;

        [SetUp]
        public void Setup()
        {
            _board = new Board(8);
            _piece = new Queen(PlayerColor.White);
            _startLocation = new BoardCoordinate(1, 1);
        }

        [TestFixture]
        public class Get_Moves : QueenTests
        {
            [Test()]
            public void Returns_Three_Corners_From_1_1()
            {
                var legalMoves = _piece.GetLegalMovesFromCoordinate(_startLocation, _board.BoardSize);
                var corners = new List<BoardCoordinate>();
                corners.Add(new BoardCoordinate(1, 8));
                corners.Add(new BoardCoordinate(8, 8));
                corners.Add(new BoardCoordinate(8, 1));

                Assert.IsTrue(corners.Intersect(legalMoves).Count() == 3);
            }

            [Test()]
            public void Returns_Three_Corners_From_8_8()
            {
                var legalMoves = _piece.GetLegalMovesFromCoordinate(new BoardCoordinate(8, 8), _board.BoardSize);
                var corners = new List<BoardCoordinate>();
                corners.Add(new BoardCoordinate(1, 8));
                corners.Add(new BoardCoordinate(1, 1));
                corners.Add(new BoardCoordinate(8, 1));

                Assert.IsTrue(corners.Intersect(legalMoves).Count() == 3);
            }

            [Test()]
            public void Returns_Three_Nearest_From_1_1()
            {
                var legalMoves = _piece.GetLegalMovesFromCoordinate(_startLocation, _board.BoardSize);
                var corners = new List<BoardCoordinate>();
                corners.Add(new BoardCoordinate(1, 2));
                corners.Add(new BoardCoordinate(2, 2));
                corners.Add(new BoardCoordinate(2, 1));

                Assert.IsTrue(corners.Intersect(legalMoves).Count() == 3);
            }
        }
    }
}
