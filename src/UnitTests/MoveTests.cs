using System.Net.NetworkInformation;
using FluentAssertions;
using Krieger;

namespace UnitTests;

public class MoveTests
{
    private static void ExecuteMoveTest(BoardCoordinate start, PlayerColor color, string pieceNotation,
        Func<BoardCoordinate, IEnumerable<BoardCoordinate>> getExpected)
    {
        var board = new Board();
        board.AddPiece(color, pieceNotation, start);
        var piece = board.GetPiece(start);

        if (piece is Pawn &&
            ((piece.Color == PlayerColor.Black && start.Row != "7") ||
             piece.Color == PlayerColor.White && start.Row != "2"))
        {
            piece.HasMoved = true;
        }
        
        var legalMoves = piece!.GetLegalMovesFromCoordinate(start, Board.MaxDistance).ToArray();

        var expected = getExpected(start).ToArray();

        var diff = legalMoves.Except(expected).ToArray();

        legalMoves.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void CanExecuteRookMoves()
    {
        ExecuteMoveTest(new BoardCoordinate("a1"), PlayerColor.Black, PieceNotation.Rook, (_) =>
        {
            var expected = new List<BoardCoordinate>();
            for (var i = 1; i < 8; i++)
            {
                expected.Add(new BoardCoordinate(0, i));
                expected.Add(new BoardCoordinate(i, 0));
            }

            return expected;
        });
    }

    [Theory]
    [InlineData("d4", 42, 44, 37, 21, 12, 10, 33, 17)]
    [InlineData("b7", 59, 43, 32, 34)]
    public void CanExecuteKnightMoves(string startLocation, params int[] exptected)
    {
        // D4 = X:3, Y:3 = Index:27
        ExecuteMoveTest(new BoardCoordinate(startLocation), PlayerColor.Black, PieceNotation.Knight, (_) =>
        {
            return exptected.Select(x => new BoardCoordinate(x));
        });
    }

    [Theory]
    [InlineData("d4", 00, 09, 18, 36, 45, 54, 63, 06, 13, 20, 34, 41, 48)]
    [InlineData("c2", 01, 19, 28, 37, 46, 55, 03, 17, 24)]
    [InlineData("g7", 63, 45, 36, 27, 18, 09, 00, 47, 61)]
    public void CanExecuteBishopMoves(string startLocation, params int[] expected)
    {
        ExecuteMoveTest(new BoardCoordinate(startLocation), PlayerColor.Black, PieceNotation.Bishop, (_) =>
        {
            return expected.Select(x => new BoardCoordinate(x));
        });
    }

    [Theory]
    [InlineData("d4", 03, 11, 19, 35, 43, 51, 59, 24, 25, 26, 28, 29, 30, 31, 18, 09, 00, 36, 45, 54, 63, 06, 13, 20, 34, 41, 48)]
    [InlineData("c2", 01, 09, 17, 18, 19, 11, 03, 02, 08, 12, 13, 14, 15, 26, 34, 42, 50, 58, 24, 28, 37, 46, 55)]
    [InlineData("g7", 06, 14, 22, 30, 38, 46, 62, 48, 49, 50, 51, 52, 53, 55, 63, 45, 36, 27, 18, 09, 00, 47, 61)]
    public void CanExecuteQueenMoves(string startLocation, params int[] expected)
    {
        ExecuteMoveTest(new BoardCoordinate(startLocation), PlayerColor.Black, PieceNotation.Queen, (_) =>
        {
            return expected.Select(x => new BoardCoordinate(x));
        });
    }

    [Theory]
    [InlineData("d4", 18, 26, 34, 35, 36, 28, 20, 19)]
    [InlineData("c2", 01, 09, 17, 18, 19, 11, 03, 02)]
    [InlineData("g7", 45, 53, 61, 62, 63, 55, 47, 46)]
    public void CanExecuteKingMoves(string startLocation, params int[] expected)
    {
        ExecuteMoveTest(new BoardCoordinate(startLocation), PlayerColor.Black, PieceNotation.King, (_) =>
        {
            return expected.Select(x => new BoardCoordinate(x));
        });
    }

    [Theory]
    [InlineData("d7", PlayerColor.Black, 43, 35, 42, 44)]
    [InlineData("d7", PlayerColor.White, 59, 58, 60)]
    [InlineData("a2", PlayerColor.White, 16, 24, 17)]
    [InlineData("a2", PlayerColor.Black, 00, 01)]
    public void CanExecutePawnMoves(string startLocation, PlayerColor color, params int[] expected)
    {
        ExecuteMoveTest(new BoardCoordinate(startLocation), color, PieceNotation.Pawn, (_) =>
        {
            return expected.Select(x => new BoardCoordinate(x));
        });
    }
}