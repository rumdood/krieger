using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krieger
{
    public interface IReferee
    {
        MoveResult MovePiece(Board board, BoardCoordinate start, BoardCoordinate end);
    }
}
