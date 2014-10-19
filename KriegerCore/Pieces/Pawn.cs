using System;
using System.Collections.Generic;

namespace Krieger
{
    public class Pawn : Piece
    {
        public Pawn(PlayerColor color) : base(color) { }

        public override IEnumerable<BoardCoordinate> GetLegalMovesFromCoordinate(BoardCoordinate origin, int boardSize)
        {
            yield return new BoardCoordinate(origin.XCoordinate, origin.YCoordinate + 1); // forward 1
            yield return new BoardCoordinate(origin.XCoordinate + 1, origin.YCoordinate + 1); // capture diagonal right
            yield return new BoardCoordinate(origin.XCoordinate - 1, origin.YCoordinate + 1); // capture diagonal left

            if (!HasMoved)
            {
                yield return new BoardCoordinate(origin.XCoordinate, origin.YCoordinate + 2); // double-move
            }
        }
    }
}
