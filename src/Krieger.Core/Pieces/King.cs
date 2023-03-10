namespace Krieger;

public class King : Piece, IPiece
{
    public string Notation => PieceNotation.King;
    public string PieceId => $"{Color}_{Notation}";
    public BoardCoordinate StartingLocation { get; init; }
    
    public King()
    {
        MoveLimit = 1;
        AvailableDirections = new List<MoveDirection>
        {
            MoveDirection.Diagonal,
            MoveDirection.Horizontal,
            MoveDirection.Vertical,
        };
    }
}