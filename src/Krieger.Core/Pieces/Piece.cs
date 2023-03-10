namespace Krieger;

internal enum Axis
{
    Horizontal,
    Vertical
}

public abstract class Piece
{
    protected int MoveLimit = 0;
    protected List<MoveDirection> AvailableDirections = new List<MoveDirection>();
    
    public bool HasMoved { get; set; }
    public PlayerColor Color { get; init; }
    
    public IEnumerable<BoardCoordinate> GetLegalMovesFromCoordinate(BoardCoordinate origin, int boardSize = 8)
    {
        var squares = new List<BoardCoordinate>();
        foreach (var direction in AvailableDirections)
        {
            switch (direction)
            {
                case MoveDirection.Diagonal:
                    squares.AddRange(GetDiagonalMovesFromCoordinate(origin));
                    break;
                case MoveDirection.Horizontal:
                    squares.AddRange(GetStraightMovesFromCoordinate(origin, Axis.Horizontal));
                    break;
                case MoveDirection.Strange:
                    squares.AddRange(GetKnightlyMovesFromCoordinate(origin));
                    break;
                case MoveDirection.Vertical:
                    squares.AddRange(GetStraightMovesFromCoordinate(origin, Axis.Vertical));
                    break;
                case MoveDirection.Pawn:
                    squares.AddRange(GetPawnMovesFromCoordinate(origin));
                    break;
                default:
                    throw new InvalidOperationException("Unknown Move Direction");
            }
        }

        return squares;
    }

    private IEnumerable<BoardCoordinate> GetStraightMovesFromCoordinate(BoardCoordinate origin, Axis forAxis)
    {
        var isVertical = forAxis == Axis.Vertical;
        var originalValueForAxis = isVertical ? origin.YCoordinate : origin.XCoordinate;

        var startingPoint = MoveLimit == 0
            ? 0
            : originalValueForAxis - MoveLimit;

        var maxDistance = MoveLimit == 0
            ? Board.MaxDistance
            : originalValueForAxis + MoveLimit + 1;
        
        for (var i = startingPoint; i < maxDistance; i++)
        {
            if (i == originalValueForAxis)
            {
                continue;
            }

            yield return isVertical
                ? new BoardCoordinate(origin.XCoordinate, i)
                : new BoardCoordinate(i, origin.YCoordinate);
        }
    }

    private IEnumerable<BoardCoordinate> GetDiagonalMovesFromCoordinate(BoardCoordinate origin)
    {
        var startingPoint = MoveLimit == 0
            ? 0
            : origin.YCoordinate - MoveLimit;

        var maxDistance = MoveLimit == 0
            ? Board.MaxDistance
            : origin.YCoordinate + MoveLimit + 1;
        
        for (var i = startingPoint; i < maxDistance; i++)
        {
            if (i == origin.YCoordinate)
            {
                continue;
            }
                
            var yDiff = Math.Abs(i - origin.YCoordinate);

            var leftX = origin.XCoordinate - yDiff;
            var rightX = origin.XCoordinate + yDiff;

            if (leftX > -1)
            {
                yield return new BoardCoordinate(leftX, i);
            }

            if (rightX < 8)
            {
                yield return new BoardCoordinate(rightX, i);
            }
        }
    }

    private static IEnumerable<BoardCoordinate> GetKnightlyMovesFromCoordinate(BoardCoordinate origin)
    {
        for (var currentX = origin.XCoordinate - 2; currentX < origin.XCoordinate + 3; currentX++)
        {
            if (currentX >= Board.MaxDistance)
            {
                break;
            }

            if (currentX < 0 || currentX == origin.XCoordinate)
            {
                continue;
            }

            var yDiff = Math.Abs(origin.XCoordinate - currentX) == 2 ? 1 : 2;
            var upY = origin.YCoordinate + yDiff;
            var downY = origin.YCoordinate - yDiff;

            if (upY < Board.MaxDistance)
            {
                yield return new BoardCoordinate(currentX, upY);
            }

            if (downY > 0)
            {
                yield return new BoardCoordinate(currentX, downY);
            }
        }
    }
    
    private IEnumerable<BoardCoordinate> GetPawnMovesFromCoordinate(BoardCoordinate origin)
    {
        var colorValue = Color == PlayerColor.White ? 1 : -1;

        if (origin.YCoordinate + colorValue is < 0 or >= Board.MaxDistance)
        {
            yield break;
        }

        yield return origin with {YCoordinate = origin.YCoordinate + colorValue}; // forward 1

        if (origin.XCoordinate < Board.MaxDistance - 1)
        {
            yield return
                new BoardCoordinate(origin.XCoordinate + 1, origin.YCoordinate + colorValue); // capture diagonal right
        }

        if (origin.XCoordinate > 0)
        {
            yield return
                new BoardCoordinate(origin.XCoordinate - 1, origin.YCoordinate + colorValue); // capture diagonal left
        }

        if (!HasMoved)
        {
            yield return origin with {YCoordinate = origin.YCoordinate + 2 * colorValue};
        }
        
        // TODO: Implement en-passant
    }
}
