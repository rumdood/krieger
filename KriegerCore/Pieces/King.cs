using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krieger
{
    public class King : Piece
    {
        public King(PlayerColor color) : base(color)
        {
            _moveLimit = 1;
        }

        public override IEnumerable<BoardCoordinate> GetLegalMovesFromCoordinate(BoardCoordinate origin, int boardSize)
        {
            var straight = base.getStraightMovesFromCoordinate(origin, _moveLimit);
            var diagonal = base.getDiagonalMovesFromCoordinate(origin, _moveLimit);
            var allLegalMoves = straight.Union(diagonal).Where(bc => bc.IsValidForBoard(boardSize));

            return allLegalMoves;
        }
    }
}
