namespace Krieger;

public class Bishop : Piece, IPiece
{
    public string Notation => PieceNotation.Bishop;
    public string PieceId => $"{Color}_{Notation}_{StartingLocation.Notation}";
    public BoardCoordinate StartingLocation { get; init; }

    public Bishop()
    {
        AvailableDirections = new List<MoveDirection>
        {
            MoveDirection.Diagonal,
        };
    }
}