using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krieger.Entities;
using NUnit.Framework;
namespace Krieger.Tests
{
    [TestFixture()]
    public class RefereeTests
    {
        private Referee _referee;
        private Board _board;
        private Queen _whiteQueen;
        private Queen _blackQueen;
        private BoardCoordinate _whiteStart;
        private BoardCoordinate _blackStart;

        [SetUp]
        public void Setup()
        {
            _board = new Board(8);
            _whiteQueen = new Queen(PlayerColor.White);
            _blackQueen = new Queen(PlayerColor.Black);
            _referee = new Referee();
            _whiteStart = new BoardCoordinate(1, 1);

            _board.AddPiece(_whiteQueen, _whiteStart);
        }

        [TestFixture]
        public class MovePiece : RefereeTests
        {
            [Test()]
            public void Move_Piece_To_Invalid_Location_Returns_No()
            {
                var moveResult = _referee.MovePiece(_board, _whiteStart, new BoardCoordinate(2, 3));
                Assert.AreEqual(MoveResult.No, moveResult);
            }

            [Test]
            public void Move_Piece_Through_Other_Piece_Vertically_Returns_No()
            {
                _blackStart = new BoardCoordinate(1, 4);
                _board.AddPiece(_blackQueen, _blackStart);

                var moveResult = _referee.MovePiece(_board, _whiteStart, new BoardCoordinate(1, 8));
                Assert.AreEqual(MoveResult.No, moveResult);
            }

            [Test]
            public void Move_Piece_Through_Other_Piece_Horizontally_Returns_No()
            {
                _blackStart = new BoardCoordinate(4, 1);
                _board.AddPiece(_blackQueen, _blackStart);

                var moveResult = _referee.MovePiece(_board, _whiteStart, new BoardCoordinate(8, 1));
                Assert.AreEqual(MoveResult.No, moveResult);
            }

            [Test]
            public void Move_Piece_Through_Other_Piece_Diagonally_Returns_No()
            {
                _blackStart = new BoardCoordinate(4, 4);
                _board.AddPiece(_blackQueen, _blackStart);

                var moveResult = _referee.MovePiece(_board, _whiteStart, new BoardCoordinate(8, 8));
                Assert.AreEqual(MoveResult.No, moveResult);
            }

            [Test]
            public void Move_Knight_Through_Other_Piece_Returns_Yes()
            {
                _blackStart = new BoardCoordinate(2, 2);
                _board.AddPiece(_blackQueen, _blackStart);

                var knight = new Knight(PlayerColor.White);
                var knightStart = new BoardCoordinate(2, 1);
                
                _board.AddPiece(knight, knightStart);

                var destination = new BoardCoordinate(3, 3);
                var moveResult = _referee.MovePiece(_board, knightStart, destination);

                Assert.AreEqual(MoveResult.Yes, moveResult);
            }

            [Test]
            public void Move_Piece_To_Unoccupied_Square_Succeeds()
            {
                _blackStart = new BoardCoordinate(1, 4);
                _board.AddPiece(_blackQueen, _blackStart);

                var destination = new BoardCoordinate(8, 1);
                var moveResult = _referee.MovePiece(_board, _whiteStart, destination);
                var endPiece = _board.GetPiece(destination);

                Assert.IsTrue(moveResult == MoveResult.Yes && endPiece == _whiteQueen);
            }

            [Test]
            public void Move_Piece_Backwards_To_Unoccupied_Square_Succeeds()
            {
                _blackStart = new BoardCoordinate(1, 4);
                _board.AddPiece(_blackQueen, _blackStart);

                var destination = new BoardCoordinate(8, 1);
                _referee.MovePiece(_board, _whiteStart, destination);
                var moveBackResult = _referee.MovePiece(_board, destination, _whiteStart);

                var endPiece = _board.GetPiece(_whiteStart);

                Assert.IsTrue(moveBackResult == MoveResult.Yes && endPiece == _whiteQueen);
            }


            [Test]
            public void Move_Piece_To_Opponent_Occupied_Square_Captures()
            {
                _blackStart = new BoardCoordinate(1, 4);
                _board.AddPiece(_blackQueen, _blackStart);

                var moveResult = _referee.MovePiece(_board, _whiteStart, _blackStart);
                var endPiece = _board.GetPiece(_blackStart);

                Assert.IsTrue(moveResult == MoveResult.Capture && endPiece == _whiteQueen);
            }
        }
    }
}
