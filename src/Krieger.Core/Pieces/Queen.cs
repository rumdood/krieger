namespace Krieger;

public class Queen : Piece, IPiece
{
    public string Notation => PieceNotation.Queen;
    public string PieceId => $"{Color}_{Notation}";
    public BoardCoordinate StartingLocation { get; init; }

    public Queen()
    {
        AvailableDirections = new List<MoveDirection>
        {
            MoveDirection.Diagonal,
            MoveDirection.Horizontal,
            MoveDirection.Vertical,
        };
    }
}