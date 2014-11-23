using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krieger.Entities;
using NUnit.Framework;
namespace Krieger.Entities.Tests
{
    [TestFixture()]
    public class RefereeTests
    {
        private static Board _board;
        private static Rook _rook;
        private static King _king;
        private static IReferee _referee;

        [SetUp]
        public void BeforeTests()
        {
            _board = new Board(8);
            _rook = new Rook(PlayerColor.White);
            _king = new King(PlayerColor.Black);
            _referee = new Referee(new MoveAttemptHandler());
        }

        [TestFixture]
        public class MovePiece : RefereeTests
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
                var move = new MoveAttempt(_board, _whiteStart, new BoardCoordinate(2, 3));
                var moveResult = _referee.TryMove(move);

                Assert.AreEqual(MoveResult.Failed(), moveResult);
            }

            [Test]
            public void Returns_No_When_Vertical_Move_Is_Blocked()
            {
                _blackStart = new BoardCoordinate(1, 4);
                _board.AddPiece(_blackQueen, _blackStart);

                var move = new MoveAttempt(_board, _whiteStart, new BoardCoordinate(1, 8));
                var moveResult = _referee.TryMove(move);
                Assert.AreEqual(MoveResult.Failed(), moveResult);
            }

            [Test]
            public void Returns_No_When_Horizontal_Move_Is_Blocked()
            {
                _blackStart = new BoardCoordinate(4, 1);
                _board.AddPiece(_blackQueen, _blackStart);

                var move = new MoveAttempt(_board, _whiteStart, new BoardCoordinate(8, 1));
                var moveResult = _referee.TryMove(move);
                Assert.AreEqual(MoveResult.Failed(), moveResult);
            }

            [Test]
            public void Returns_No_When_Diagonal_Move_Is_Blocked()
            {
                _blackStart = new BoardCoordinate(4, 4);
                _board.AddPiece(_blackQueen, _blackStart);

                var move = new MoveAttempt(_board, _whiteStart, new BoardCoordinate(8, 8));
                var moveResult = _referee.TryMove(move);
                Assert.AreEqual(MoveResult.Failed(), moveResult);
            }

            [Test]
            public void Returns_No_When_Diagonal_Move_Is_Blocked_From_Non_1_1_Start()
            {
                var newWhiteStart = new BoardCoordinate(2, 1);
                _board.RemovePiece(_whiteStart);
                _board.AddPiece(_whiteQueen, newWhiteStart);
                _blackStart = new BoardCoordinate(3, 2);
                _board.AddPiece(_blackQueen, _blackStart);

                var move = new MoveAttempt(_board, newWhiteStart, new BoardCoordinate(4, 3));
                var moveResult = _referee.TryMove(move);
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
                var move = new MoveAttempt(_board, knightStart, destination);
                var moveResult = _referee.TryMove(move);

                Assert.AreEqual(MoveResult.Succeeded(), moveResult);
            }

            [Test]
            public void Returns_Yes_When_Moving_To_Unoccupied_Square()
            {
                _blackStart = new BoardCoordinate(1, 4);
                _board.AddPiece(_blackQueen, _blackStart);

                var destination = new BoardCoordinate(8, 1);
                var move = new MoveAttempt(_board, _whiteStart, destination);
                var moveResult = _referee.TryMove(move);
                var endPiece = _board.GetPiece(destination);

                Assert.IsTrue(moveResult == MoveResult.Succeeded() && endPiece == _whiteQueen);
            }

            [Test]
            public void Returns_Capture_When_Move_To_Opponent_Occupied_Square()
            {
                _blackStart = new BoardCoordinate(1, 4);
                _board.AddPiece(_blackQueen, _blackStart);

                var move = new MoveAttempt(_board, _whiteStart, _blackStart);
                var moveResult = _referee.TryMove(move);
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

                var move = new MoveAttempt(_board, _blackStart, new BoardCoordinate(2, 4));
                var moveResult = _referee.TryMove(move);
                Assert.AreEqual(MoveResult.Failed(), moveResult);
            }

            [Test]
            public void Returns_Yes_Moving_King_Out_Of_Check()
            {
                var king = new King(PlayerColor.Black);
                var kingStart = new BoardCoordinate(1, 8);
                _board.AddPiece(king, kingStart);

                var move = new MoveAttempt(_board, kingStart, new BoardCoordinate(2, 8));
                var moveResult = _referee.TryMove(move);
                Assert.AreEqual(MoveResult.Succeeded(), moveResult);
            }

            [Test]
            public void Returns_Yes_When_Blocking_Check()
            {
                var king = new King(PlayerColor.Black);
                var kingStart = new BoardCoordinate(1, 8);
                _board.AddPiece(king, kingStart);

                _blackStart = new BoardCoordinate(2, 4);
                _board.AddPiece(_blackQueen, _blackStart);

                var move = new MoveAttempt(_board, _blackStart, new BoardCoordinate(1, 4));
                var moveResult = _referee.TryMove(move);
                Assert.AreEqual(MoveResult.Succeeded(), moveResult);
            }

            [Test]
            public void Returns_Capture_When_Capturing_Check()
            {
                var king = new King(PlayerColor.Black);
                var kingStart = new BoardCoordinate(1, 8);
                _board.AddPiece(king, kingStart);

                _blackStart = new BoardCoordinate(8, 1);
                _board.AddPiece(_blackQueen, _blackStart);

                var move = new MoveAttempt(_board, _blackStart, _whiteStart);
                var moveResult = _referee.TryMove(move);
                Assert.AreEqual(MoveResult.Captured(_whiteQueen), moveResult);
            }

            [Test]
            public void Returns_No_When_Not_Solving_Check()
            {
                var king = new King(PlayerColor.Black);
                var kingStart = new BoardCoordinate(1, 8);
                _board.AddPiece(king, kingStart);

                _blackStart = new BoardCoordinate(2, 4);
                _board.AddPiece(_blackQueen, _blackStart);

                var move = new MoveAttempt(_board, _blackStart, new BoardCoordinate(2, 8));
                var moveResult = _referee.TryMove(move);
                Assert.AreEqual(MoveResult.Failed(), moveResult);
            }
        }
    }
}
