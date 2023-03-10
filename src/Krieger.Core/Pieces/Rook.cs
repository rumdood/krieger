namespace Krieger;

public class Rook: Piece, IPiece
{
    public string Notation => PieceNotation.Rook;
    public string PieceId => $"{Color}_{Notation}_{StartingLocation.Notation}";
    public BoardCoordinate StartingLocation { get; init; }

    public Rook()
    {
        AvailableDirections = new List<MoveDirection>
        {
            MoveDirection.Horizontal,
            MoveDirection.Vertical
        };
    }
}