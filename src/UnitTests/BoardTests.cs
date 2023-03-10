using Krieger;
using System.Linq;

namespace UnitTests;

public class BoardTests
{
    private static Board GenerateBoardWithPiecesAtStartingPosition()
    {
        var board = new Board();
        board.AddPiece(PlayerColor.White, PieceNotation.Rook, new BoardCoordinate(0, 0));
        board.AddPiece(PlayerColor.White, PieceNotation.Knight, new BoardCoordinate(1, 0));
        board.AddPiece(PlayerColor.White, PieceNotation.Bishop, new BoardCoordinate(2, 0));
        board.AddPiece(PlayerColor.White, PieceNotation.Queen, new BoardCoordinate(3, 0));
        board.AddPiece(PlayerColor.White, PieceNotation.King, new BoardCoordinate(4, 0));
        board.AddPiece(PlayerColor.White, PieceNotation.Bishop, new BoardCoordinate(5, 0));
        board.AddPiece(PlayerColor.White, PieceNotation.Knight, new BoardCoordinate(6, 0));
        board.AddPiece(PlayerColor.White, PieceNotation.Rook, new BoardCoordinate(7, 0));
        
        board.AddPiece(PlayerColor.Black, PieceNotation.Rook, new BoardCoordinate(0, 7));
        board.AddPiece(PlayerColor.Black, PieceNotation.Knight, new BoardCoordinate(1, 7));
        board.AddPiece(PlayerColor.Black, PieceNotation.Bishop, new BoardCoordinate(2, 7));
        board.AddPiece(PlayerColor.Black, PieceNotation.Queen, new BoardCoordinate(3, 7));
        board.AddPiece(PlayerColor.Black, PieceNotation.King, new BoardCoordinate(4, 7));
        board.AddPiece(PlayerColor.Black, PieceNotation.Bishop, new BoardCoordinate(5, 7));
        board.AddPiece(PlayerColor.Black, PieceNotation.Knight, new BoardCoordinate(6, 7));
        board.AddPiece(PlayerColor.Black, PieceNotation.Rook, new BoardCoordinate(7, 7));

        for (var i = 0; i < 8; i++)
        {
            board.AddPiece(PlayerColor.White, PieceNotation.Pawn, new BoardCoordinate(i, 1));
            board.AddPiece(PlayerColor.Black, PieceNotation.Pawn, new BoardCoordinate(i, 6));
        }

        return board;
    }
    
    [Fact]
    public void CoordinateValuesAreCorrect()
    {
        var bc = new BoardCoordinate(2, 4);
        Assert.Equal(34, bc.Index);
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(0, -1)]
    [InlineData(8, 0)]
    [InlineData(0, 8)]
    [InlineData(50, 7)]
    [InlineData(-1, 8)]
    public void CannotAddPieceOutsideLegalBoard(int x, int y)
    {
        var board = new Board();
        Assert.Throws<InvalidPlacementException>(() =>
            board.AddPiece(PlayerColor.White, PieceNotation.Rook, new BoardCoordinate(x, y)));
    }

    [Fact]
    public void EqualityOfBoardCoordinatesWorks()
    {
        var bc1 = new BoardCoordinate(0, 0);
        var bc2 = new BoardCoordinate(0, 0);
        var bc3 = new BoardCoordinate(0, 1);

        Assert.Equal(bc1, bc2);
        Assert.NotEqual(bc1, bc3);
    }

    [Fact]
    public void CannotPlacePieceOnOccupiedSquare()
    {
        var board = new Board();
        board.AddPiece(PlayerColor.Black, PieceNotation.Bishop, new BoardCoordinate(0, 0));
        Assert.Throws<InvalidPlacementException>(() =>
            board.AddPiece(PlayerColor.Black, PieceNotation.Bishop, new BoardCoordinate(0, 0)));
    }
    
    [Theory]
    [InlineData(0, 0)]
    [InlineData(0, 1)]
    [InlineData(7, 0)]
    [InlineData(0, 7)]
    [InlineData(7, 7)]
    [InlineData(3, 5)]
    public void CanPlacePiecesOnBoard(int x, int y)
    {
        var board = new Board();
        board.AddPiece(PlayerColor.White, PieceNotation.Rook, new BoardCoordinate(x, y));
    }

    [Theory]
    [InlineData("B3", 17)]
    [InlineData("C7", 50)]
    [InlineData("d2", 11)]
    public void CanCreateBoardCoordinateFromNotation(string notation, int expectedIndex)
    {
        var bc = new BoardCoordinate(notation);
        Assert.Equal(expectedIndex, bc.Index);
    }
}