using System;
using System.Collections.Generic;

namespace Krieger
{
    public class Pawn : Piece
    {
        public Pawn(PlayerColor color) : base(color) { }

        public override IEnumerable<BoardCoordinate> GetLegalMovesFromCoordinate(BoardCoordinate origin, int boardSize)
        {
            int colorValue = 1;
            
            if (Color == PlayerColor.Black)
            {
                colorValue = -1;
            }

            yield return new BoardCoordinate(origin.XCoordinate, origin.YCoordinate + (1*colorValue)); // forward 1
            yield return new BoardCoordinate(origin.XCoordinate + 1, origin.YCoordinate + (1 * colorValue)); // capture diagonal right
            yield return new BoardCoordinate(origin.XCoordinate - 1, origin.YCoordinate + (1 * colorValue)); // capture diagonal left

            if (!HasMoved)
            {
                yield return new BoardCoordinate(origin.XCoordinate, origin.YCoordinate + (2 * colorValue)); // double-move
            }
        }
    }
}
