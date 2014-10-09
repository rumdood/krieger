using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Krieger;
using NUnit.Framework;

namespace Krieger.Tests
{
    [TestFixture]
    public class BoardTests
    {
        private static Board _board;
        private static Pawn _piece;

        [SetUp]
        public void BeforeTests()
        {
            _board = new Board();
            _piece = new Pawn();
        }

        [Test, Category("AddPiece")]
        public void Add_Piece_To_Unoccupied_Square_Does_Not_ThrowException()
        {
            var boardCoordinate = new BoardCoordinate(2, 1);
            Assert.DoesNotThrow(() => _board.AddPiece(_piece, boardCoordinate), "Adding a piece to an unoccupied location should not throw an exception");
        }

        [Test, Category("AddPiece")]
        public void Add_Piece_To_Occupied_Square_Throws_Exception()
        {
            var boardCoordinate = new BoardCoordinate(2, 1);
            _board.AddPiece(_piece, boardCoordinate);
            Assert.Throws(typeof(InvalidOperationException), () => _board.AddPiece(new Pawn(), boardCoordinate));
        }

        [Test, Category("GetPiece")]
        public void Gets_Piece_That_Has_Been_Added()
        {
            var boardCoordinate = new BoardCoordinate(2, 1);
            _board.AddPiece(_piece, boardCoordinate);
            var gottenPiece = _board.GetPiece(boardCoordinate);
            Assert.AreEqual(_piece, gottenPiece);
        }

        [Test, Category("GetPiece")]
        public void GetPiece_Returns_Null_If_No_Piece_At_Location()
        {
            var boardCoordinate = new BoardCoordinate(2, 1);
            var piece = _board.GetPiece(boardCoordinate);

            Assert.IsNull(piece);
        }
    }
}
