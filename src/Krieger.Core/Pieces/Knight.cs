namespace Krieger;

public class Knight : Piece, IPiece
{
    public string Notation => PieceNotation.Knight;
    public string PieceId => $"{Color}_{Notation}_{StartingLocation.Notation}";
    public BoardCoordinate StartingLocation { get; init; }

    public Knight()
    {
        AvailableDirections = new List<MoveDirection>
        {
            MoveDirection.Strange,
        };
    }
}