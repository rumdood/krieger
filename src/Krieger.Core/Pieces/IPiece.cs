namespace Krieger;

public interface IPiece
{
    bool HasMoved { get; set; }
    PlayerColor Color { get; init; }
    string PieceId { get; }
    BoardCoordinate StartingLocation { get; init; }
    string Notation { get; }
    IEnumerable<BoardCoordinate> GetLegalMovesFromCoordinate(BoardCoordinate origin, int boardSize);
}