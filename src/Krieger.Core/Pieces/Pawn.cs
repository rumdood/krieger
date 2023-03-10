namespace Krieger;

public class Pawn: Piece, IPiece
{
    public string Notation => PieceNotation.Pawn;
    public string PieceId => $"{Color}_{Notation}_{StartingLocation.Notation}";
    public BoardCoordinate StartingLocation { get; init; }
    
    public Pawn()
    {
        MoveLimit = 1;
        AvailableDirections = new List<MoveDirection>
        {
            MoveDirection.Pawn,
        };
    }
}