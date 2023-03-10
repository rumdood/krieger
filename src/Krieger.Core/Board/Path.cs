namespace Krieger;

public static class Path
{
    private static IEnumerable<BoardCoordinate> GetHorizontalSpaces(BoardCoordinate origin, BoardCoordinate destination)
    {
        var bigger = Math.Max(origin.XCoordinate, destination.XCoordinate);
        var smaller = Math.Min(origin.XCoordinate, destination.XCoordinate);

        for (var i = smaller + 1; i < bigger; i++)
        {
            yield return new BoardCoordinate(i, origin.YCoordinate);
        }
    }

    private static IEnumerable<BoardCoordinate> GetVerticalSpaces(BoardCoordinate origin, BoardCoordinate destination)
    {var bigger = Math.Max(origin.YCoordinate, destination.YCoordinate);
        var smaller = Math.Min(origin.YCoordinate, destination.YCoordinate);

        for (var i = smaller + 1; i < bigger; i++)
        {
            yield return new BoardCoordinate(origin.XCoordinate, i);
        }
    }
    
    private static IEnumerable<BoardCoordinate> GetDiagonalSpaces(BoardCoordinate origin, BoardCoordinate destination)
    {
        var distance = Math.Abs(origin.XCoordinate - destination.XCoordinate);
        var xDirection = (destination.XCoordinate - origin.XCoordinate) / distance;
        var yDirection = (destination.YCoordinate - origin.YCoordinate) / distance;

        for (var i = 1; i < distance; i++)
        {
            yield return new BoardCoordinate(
                origin.XCoordinate + (i * xDirection),
                origin.YCoordinate + (i * yDirection));
        }
    }

    public static IEnumerable<BoardCoordinate> GetSpaces(BoardCoordinate origin, BoardCoordinate destination)
    {
        return (o: origin, d: destination) switch
        {
            var (o, d) when o.XCoordinate != d.XCoordinate &&
                            o.YCoordinate == d.YCoordinate => GetHorizontalSpaces(origin, destination),
            var (o, d) when o.XCoordinate == d.XCoordinate &&
                            o.YCoordinate != d.YCoordinate => GetVerticalSpaces(origin, destination),
            _ => GetDiagonalSpaces(origin, destination)
        };
    }
}