using System;

namespace Krieger
{
    public enum PlayerColor
    {
        White,
        Black
    }

    public enum MoveResult
    {
        No,
        Yes,
        Capture
    }

    public enum MoveDirection
    {
        Horizontal,
        Vertical,
        Diagonal,
        Strange
    }
}
