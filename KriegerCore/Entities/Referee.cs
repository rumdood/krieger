using System;
using System.Linq;

namespace Krieger.Entities
{
    public class Referee : IReferee
    {
        public MoveResult TryMovePiece(Board board, BoardCoordinate start, BoardCoordinate end)
        {
            var result = board.MovePiece(start, end);
            return result;
        }
    }
}
