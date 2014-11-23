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
        private static Rook _rook;
        private static King _king;

        [SetUp]
        public void BeforeTests()
        {
            _board = new Board(8);
            _rook = new Rook(PlayerColor.White);
            _king = new King(PlayerColor.Black);
        }

        [TestFixture]
        public class Add_Piece : BoardTests
        {
            [Test, Category("AddPiece")]
            public void Add_Piece_To_Unoccupied_Square_Does_Not_ThrowException()
            {
                var boardCoordinate = new BoardCoordinate(2, 1);
                Assert.DoesNotThrow(() => _board.AddPiece(_rook, boardCoordinate), "Adding a piece to an unoccupied location should not throw an exception");
            }

            [Test, Category("AddPiece")]
            public void Add_Piece_To_Occupied_Square_Throws_Exception()
            {
                var boardCoordinate = new BoardCoordinate(2, 1);
                _board.AddPiece(_rook, boardCoordinate);
                Assert.Throws(typeof(InvalidOperationException), () => _board.AddPiece(new Pawn(PlayerColor.White), boardCoordinate));
            }

            [Test, Category("AddPiece")]
            public void Add_Piece_To_Invalid_Square_Throws_Exception()
            {
                var boardCoordinate = new BoardCoordinate(1, 9);
                Assert.Throws(typeof(InvalidOperationException), () => _board.AddPiece(new Pawn(PlayerColor.White), boardCoordinate));
            }
        }

        [TestFixture]
        public class Get_Piece : BoardTests
        {
            [Test, Category("GetPiece")]
            public void Gets_Piece_That_Has_Been_Added()
            {
                var boardCoordinate = new BoardCoordinate(2, 1);
                _board.AddPiece(_rook, boardCoordinate);
                var gottenPiece = _board.GetPiece(boardCoordinate);
                Assert.AreEqual(_rook, gottenPiece);
            }

            [Test, Category("GetPiece")]
            public void GetPiece_Returns_Null_If_No_Piece_At_Location()
            {
                var boardCoordinate = new BoardCoordinate(2, 1);
                var piece = _board.GetPiece(boardCoordinate);

                Assert.IsNull(piece);
            }
        }

        [TestFixture]
        public class Check_For_Check : BoardTests
        {
            [Test]
            public void Returns_False_For_Unchecked_King()
            {
                _board.AddPiece(_rook, new BoardCoordinate(1, 1));
                _board.AddPiece(_king, new BoardCoordinate(2, 2));

                Assert.IsFalse(_board.IsPlayerInCheck(PlayerColor.Black));
            }

            [Test]
            public void Returns_True_For_Checked_King()
            {
                _board.AddPiece(_rook, new BoardCoordinate(1, 1));
                _board.AddPiece(_king, new BoardCoordinate(1, 8));

                Assert.IsTrue(_board.IsPlayerInCheck(PlayerColor.Black));
            }
        }
    }
}
