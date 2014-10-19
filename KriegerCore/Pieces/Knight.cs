using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krieger
{
    public class Knight : Piece
    {
        public Knight(PlayerColor color) : base(color) { }

        public override IEnumerable<BoardCoordinate> GetLegalMovesFromCoordinate(BoardCoordinate origin, int boardSize)
        {
            return getKnightlyMoves(origin).Where(bc => bc.IsValidForBoard(boardSize));
        }

        protected IEnumerable<BoardCoordinate> getKnightlyMoves(BoardCoordinate origin)
        {
            yield return new BoardCoordinate(origin.XCoordinate + 1, origin.YCoordinate + 2);
            yield return new BoardCoordinate(origin.XCoordinate + 1, origin.YCoordinate - 2);
            yield return new BoardCoordinate(origin.XCoordinate - 1, origin.YCoordinate + 2);
            yield return new BoardCoordinate(origin.XCoordinate - 1, origin.YCoordinate - 2);
            yield return new BoardCoordinate(origin.XCoordinate + 2, origin.YCoordinate + 1);
            yield return new BoardCoordinate(origin.XCoordinate + 2, origin.YCoordinate - 1);
            yield return new BoardCoordinate(origin.XCoordinate - 2, origin.YCoordinate + 1);
            yield return new BoardCoordinate(origin.XCoordinate - 2, origin.YCoordinate - 1);
        }
    }
}
