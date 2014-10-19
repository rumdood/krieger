using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krieger
{
    public class Queen : Piece
    {
        public Queen(PlayerColor color) : base(color) { }

        public override IEnumerable<BoardCoordinate> GetLegalMovesFromCoordinate(BoardCoordinate origin, int boardSize)
        {
            var straight = getStraightMovesFromCoordinate(origin, boardSize);
            var diagonal = getDiagonalMovesFromCoordinate(origin, boardSize);
            var all = straight.Union(diagonal);
            return all.Where(bc => bc.IsValidForBoard(boardSize));
        }
    }
}
