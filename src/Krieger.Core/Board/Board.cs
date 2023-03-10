namespace Krieger;

public record MoveResultEntry(
    DateTime Timestamp, 
    PlayerColor Color, 
    MoveAttempt Attempt, 
    MoveResult Result);

public class Board
{
    public const int MaxDistance = 8;
    
    private record PieceWithLocation(IPiece Piece, BoardCoordinate CurrentLocation);

    private readonly Dictionary<BoardCoordinate, IPiece> _piecesByLocation = new();
    private readonly Dictionary<string, PieceWithLocation> _piecesById = new();

    private bool _hasWhiteCastled = false;
    private bool _hasBlackCastled = false;

    private readonly List<MoveResultEntry> _moveLog = new();
    private readonly List<IPiece> _whiteCaptures = new();
    private readonly List<IPiece> _blackCaptures = new();

    public IEnumerable<MoveResultEntry> SuccessfulMoves => _moveLog.Where(mv =>
        mv.Result.Success == MoveSuccess.Succeeded);

    public bool IsPathClear(BoardCoordinate origin, BoardCoordinate destination)
    {
        return Path.GetSpaces(origin, destination).All(square => !_piecesByLocation.ContainsKey(square));
    }

    private static bool IsCoordinateValid(BoardCoordinate coordinate)
    {
        return coordinate.XCoordinate is >= 0 and < MaxDistance
               && coordinate.YCoordinate is >= 0 and < MaxDistance;
    }

    public void AddPiece(PlayerColor color, string pieceNotation, BoardCoordinate location)
    {
        if (!IsCoordinateValid(location))
        {
            throw new InvalidPlacementException("Cannot place a piece outside the confines of the board");
        }

        if (_piecesByLocation.ContainsKey(location))
        {
            throw new InvalidPlacementException("Cannot place a piece on an already-occupied square");
        }

        IPiece piece = pieceNotation.ToUpperInvariant() switch
        {
            PieceNotation.Pawn => new Pawn {Color = color, StartingLocation = location},
            PieceNotation.Rook => new Rook {Color = color, StartingLocation = location},
            PieceNotation.Knight => new Knight {Color = color, StartingLocation = location},
            PieceNotation.Bishop => new Bishop {Color = color, StartingLocation = location},
            PieceNotation.Queen => new Queen {Color = color, StartingLocation = location},
            PieceNotation.King => new King {Color = color, StartingLocation = location},
            _ => throw new InvalidOperationException($"Unknown piece notation {pieceNotation}")
        };

        _piecesByLocation[location] = piece;
        _piecesById[piece.PieceId] = new PieceWithLocation(piece, location);
    }

    public void RemovePiece(BoardCoordinate location)
    {
        if (_piecesByLocation.TryGetValue(location, out var piece))
        {
            if (piece.Color == PlayerColor.White)
            {
                _blackCaptures.Add(piece);
            }
            else
            {
                _whiteCaptures.Add(piece);
            }
        }
        _piecesByLocation.Remove(location);
    }

    public IPiece? GetPiece(BoardCoordinate location)
    {
        _ = _piecesByLocation.TryGetValue(location, out var piece);

        return piece;
    }

    public bool IsPlayerInCheck(PlayerColor color)
    {
        var king = _piecesById[$"{color}_{PieceNotation.King}"];
        var enemyPieces = _piecesById.Where(kv => !kv.Key.StartsWith(color.ToString())).Select(kv => kv.Value);

        foreach (var enemy in enemyPieces)
        {
            if (enemy.Piece is Knight || 
                !enemy.Piece.GetLegalMovesFromCoordinate(enemy.CurrentLocation, MaxDistance)
                    .Contains(king.CurrentLocation))
            {
                continue;
            }
            
            if (IsPathClear(enemy.CurrentLocation, king.CurrentLocation))
            {
                return true;
            }
        }

        return false;
    }

    public Board CloneBoard()
    {
        var clone = new Board();

        foreach (var pieceLocation in _piecesById.Values)
        {
            clone.AddPiece(pieceLocation.Piece.Color, pieceLocation.Piece.Notation, pieceLocation.CurrentLocation);
        }

        return clone;
    }
}