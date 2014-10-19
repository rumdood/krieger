using System;
using System.Collections.Generic;
using System.Linq;
using Krieger;
using NUnit.Framework;

namespace Krieger.Tests
{
    [TestFixture()]
    public class PawnTests
    {
        private Board _board;
        private Pawn _pawn;
        private BoardCoordinate _startingLocation;

        [SetUp]
        public void Setup()
        {
            _board = new Board(8);
            _pawn = new Pawn(PlayerColor.White);
            _startingLocation = new BoardCoordinate(1, 1);
        }

        [TestFixture]
        public class Get_Moves : PawnTests
        {
            [Test(), Category("GetLegalMoves")]
            public void Returns_1_2_When_Passed_1_1()
            {
                var legalMoves = _pawn.GetLegalMovesFromCoordinate(_startingLocation, _board.BoardSize);

                var target = new BoardCoordinate(1, 2);
                Assert.IsTrue(legalMoves.Contains(target));
            }

            [Test(), Category("GetLegalMoves")]
            public void Returns_1_3_When_Passed_1_1_And_Has_Not_Moved()
            {
                var legalMoves = _pawn.GetLegalMovesFromCoordinate(_startingLocation, _board.BoardSize);

                var target = new BoardCoordinate(1, 3);
                Assert.IsTrue(legalMoves.Contains(target));
            }
        }
    }
}
