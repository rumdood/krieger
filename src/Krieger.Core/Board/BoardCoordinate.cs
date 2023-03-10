using System.Linq;

namespace Krieger;

public readonly record struct BoardCoordinate(int XCoordinate, int YCoordinate)
{
    private readonly char[] _columns = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

    public string Notation => $"{_columns[XCoordinate]}{YCoordinate + 1}";
    public string Column => _columns[XCoordinate].ToString();
    public string Row => (YCoordinate + 1).ToString();
    public int Index => (YCoordinate * 8) + XCoordinate;

    public BoardCoordinate(string rankFile) : this(char.ToLower(rankFile[0]) - 'a', rankFile[1] - '1')
    {
    }

    public BoardCoordinate(int index) : this(index % 8, index / 8)
    {
    }

    public int StraightLineDistanceFrom(BoardCoordinate other)
    {
        return Math.Max(Math.Abs(XCoordinate - other.XCoordinate), Math.Abs(YCoordinate - other.YCoordinate));
    }

    public bool Equals(BoardCoordinate other)
    {
        return Index == other.Index;
    }

    public override int GetHashCode()
    {
        return Index.GetHashCode();
    }
}