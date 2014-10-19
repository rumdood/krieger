using System;
using System.Collections.Generic;

namespace Krieger
{
    public interface IPiece
    {
        IEnumerable<BoardCoordinate> GetLegalMovesFromCoordinate(BoardCoordinate origin, int boardSize);
    }
}
