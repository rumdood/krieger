using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krieger
{
    public class Bishop : Piece
    {
        public Bishop(PlayerColor color) : base(color) { }

        public override IEnumerable<BoardCoordinate> GetLegalMovesFromCoordinate(BoardCoordinate origin, int boardSize)
        {
            return getDiagonalMovesFromCoordinate(origin, boardSize).Where(bc => bc.IsValidForBoard(boardSize));
        }
    }
}
