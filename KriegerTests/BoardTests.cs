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

        [TestFixture]
        public class MovePiece : BoardTests
        {
            private Queen _whiteQueen;
            private Queen _blackQueen;
            private BoardCoordinate _whiteStart;
            private BoardCoordinate _blackStart;

            [SetUp]
            public void Setup()
            {
                _whiteQueen = new Queen(PlayerColor.White);
                _blackQueen = new Queen(PlayerColor.Black);
                _whiteStart = new BoardCoordinate(1, 1);

                _board.AddPiece(_whiteQueen, _whiteStart);
            }

            [Test()]
            public void Returns_No_When_Destination_Is_Invalid()
            {
                var moveResult = _board.MovePiece(_whiteStart, new BoardCoordinate(2, 3));
                Assert.AreEqual(MoveResult.Failed(), moveResult);
            }

            [Test]
            public void Returns_No_When_Vertical_Move_Is_Blocked()
            {
                _blackStart = new BoardCoordinate(1, 4);
                _board.AddPiece(_blackQueen, _blackStart);

                var moveResult = _board.MovePiece(_whiteStart, new BoardCoordinate(1, 8));
                Assert.AreEqual(MoveResult.Failed(), moveResult);
            }

            [Test]
            public void Returns_No_When_Horizontal_Move_Is_Blocked()
            {
                _blackStart = new BoardCoordinate(4, 1);
                _board.AddPiece(_blackQueen, _blackStart);

                var moveResult = _board.MovePiece(_whiteStart, new BoardCoordinate(8, 1));
                Assert.AreEqual(MoveResult.Failed(), moveResult);
            }

            [Test]
            public void Returns_No_When_Diagonal_Move_Is_Blocked()
            {
                _blackStart = new BoardCoordinate(4, 4);
                _board.AddPiece(_blackQueen, _blackStart);

                var moveResult = _board.MovePiece(_whiteStart, new BoardCoordinate(8, 8));
                Assert.AreEqual(MoveResult.Failed(), moveResult);
            }

            [Test]
            public void Returns_No_When_Diagonal_Move_Is_Blocked_From_Non_1_1_Start()
            {
                var newWhiteStart = new BoardCoordinate(2, 1);
                _board.MovePiece(_whiteStart, newWhiteStart);
                _blackStart = new BoardCoordinate(3, 2);
                _board.AddPiece(_blackQueen, _blackStart);

                var moveResult = _board.MovePiece(newWhiteStart, new BoardCoordinate(4, 3));
                Assert.AreEqual(MoveResult.Failed(), moveResult);
            }

            [Test]
            public void Returns_Yes_When_Knight_Move_Is_Blocked()
            {
                _blackStart = new BoardCoordinate(2, 2);
                _board.AddPiece(_blackQueen, _blackStart);

                var knight = new Knight(PlayerColor.White);
                var knightStart = new BoardCoordinate(2, 1);

                _board.AddPiece(knight, knightStart);

                var destination = new BoardCoordinate(3, 3);
                var moveResult = _board.MovePiece(knightStart, destination);

                Assert.AreEqual(MoveResult.Succeeded(), moveResult);
            }

            [Test]
            public void Returns_Yes_When_Moving_To_Unoccupied_Square()
            {
                _blackStart = new BoardCoordinate(1, 4);
                _board.AddPiece(_blackQueen, _blackStart);

                var destination = new BoardCoordinate(8, 1);
                var moveResult = _board.MovePiece(_whiteStart, destination);
                var endPiece = _board.GetPiece(destination);

                Assert.IsTrue(moveResult == MoveResult.Succeeded() && endPiece == _whiteQueen);
            }

            [Test]
            public void Returns_Capture_When_Move_To_Opponent_Occupied_Square()
            {
                _blackStart = new BoardCoordinate(1, 4);
                _board.AddPiece(_blackQueen, _blackStart);

                var moveResult = _board.MovePiece(_whiteStart, _blackStart);
                var endPiece = _board.GetPiece(_blackStart);

                Assert.IsTrue(moveResult == MoveResult.Captured(_blackQueen) && endPiece == _whiteQueen);
            }

            [Test]
            public void Returns_No_When_Exposing_King_To_Check()
            {
                var king = new King(PlayerColor.Black);
                var kingStart = new BoardCoordinate(1, 8);
                _board.AddPiece(king, kingStart);

                _blackStart = new BoardCoordinate(1, 4);
                _board.AddPiece(_blackQueen, _blackStart);

                var resultOfMove = _board.MovePiece(_blackStart, new BoardCoordinate(2, 4));
                Assert.AreEqual(MoveResult.Failed(), resultOfMove);
            }

            [Test]
            public void Returns_Yes_Moving_King_Out_Of_Check()
            {
                var king = new King(PlayerColor.Black);
                var kingStart = new BoardCoordinate(1, 8);
                _board.AddPiece(king, kingStart);

                var resultOfMove = _board.MovePiece(kingStart, new BoardCoordinate(2, 8));
                Assert.AreEqual(MoveResult.Succeeded(), resultOfMove);
            }

            [Test]
            public void Returns_Yes_When_Blocking_Check()
            {
                var king = new King(PlayerColor.Black);
                var kingStart = new BoardCoordinate(1, 8);
                _board.AddPiece(king, kingStart);

                _blackStart = new BoardCoordinate(2, 4);
                _board.AddPiece(_blackQueen, _blackStart);

                var resultOfMove = _board.MovePiece(_blackStart, new BoardCoordinate(1, 4));
                Assert.AreEqual(MoveResult.Succeeded(), resultOfMove);
            }

            [Test]
            public void Returns_Capture_When_Capturing_Check()
            {
                var king = new King(PlayerColor.Black);
                var kingStart = new BoardCoordinate(1, 8);
                _board.AddPiece(king, kingStart);

                _blackStart = new BoardCoordinate(8, 1);
                _board.AddPiece(_blackQueen, _blackStart);

                var resultOfMove = _board.MovePiece(_blackStart, _whiteStart);
                Assert.AreEqual(MoveResult.Captured(_whiteQueen), resultOfMove);
            }

            [Test]
            public void Returns_No_When_Not_Solving_Check()
            {
                var king = new King(PlayerColor.Black);
                var kingStart = new BoardCoordinate(1, 8);
                _board.AddPiece(king, kingStart);

                _blackStart = new BoardCoordinate(2, 4);
                _board.AddPiece(_blackQueen, _blackStart);

                var resultOfMove = _board.MovePiece(_blackStart, new BoardCoordinate(2, 8));
                Assert.AreEqual(MoveResult.Failed(), resultOfMove);
            }
        }
    }
}
