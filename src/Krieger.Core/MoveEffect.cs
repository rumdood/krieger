namespace Krieger;

public enum MoveEffect
{
    None = 0,
    PawnCaptured = 1,
    PieceCaptured = 2,
    Check = 3,
    Checkmate = 4,
    Stalemate = 5,
    PawnPromoted = 6,
}

public enum MoveSuccess
{
    Failed = 0,
    Succeeded = 1,
    Illegal = 2,
}

public record MoveResult(MoveSuccess Success, IEnumerable<MoveEffect> Effects);