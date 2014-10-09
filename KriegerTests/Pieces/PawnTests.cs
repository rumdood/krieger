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
    public class PawnTests
    {
        private Board _board;
        private Pawn _pawn;

        [SetUp]
        public void Setup()
        {
            _board = new Board();
            _pawn = new Pawn();

            _board.AddPiece(_pawn, new BoardCoordinate(1, 2));
        }

        [Test(), Category("GetLegalMoves")]
        public void Returns_1_3_When_Passed_1_2()
        {
            var legalMoves = _pawn.GetLegalMovesFromCoordinate(
                new BoardCoordinate(1, 2));

            var onlyMove = legalMoves.First();
            var target = new BoardCoordinate(1, 3);
            Assert.AreEqual(onlyMove, target);
        }
    }
}
