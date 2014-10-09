using System;
using System.Collections.Generic;

namespace Krieger
{
    public class Pawn : IPiece
    {
        public virtual List<BoardCoordinate> GetLegalMovesFromCoordinate(BoardCoordinate origin)
        {
            List<BoardCoordinate> possibleMoves = new List<BoardCoordinate>();
            var pawnMove = new BoardCoordinate(origin.XCoordinate, origin.YCoordinate + 1);
            possibleMoves.Add(pawnMove);
            return possibleMoves;
        }
    }
}
