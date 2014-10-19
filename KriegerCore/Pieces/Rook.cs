using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krieger
{
    public class Rook : Piece
    {
        public Rook(PlayerColor color) : base(color) { }

        public override IEnumerable<BoardCoordinate> GetLegalMovesFromCoordinate(BoardCoordinate origin, int boardSize)
        {
            return getStraightMovesFromCoordinate(origin, boardSize).Where(bc => bc.IsValidForBoard(boardSize));
        }
    }
}
