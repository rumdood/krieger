using System;
using System.Collections.Generic;

namespace Krieger
{
    public interface IPiece
    {
        List<BoardCoordinate> GetLegalMovesFromCoordinate(BoardCoordinate origin);
    }
}
