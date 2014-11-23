using System;
using System.Collections.Generic;
using System.Linq;

namespace Krieger
{
    public class MoveAttempt
    {
        private readonly BoardCoordinate _origin;
        private readonly BoardCoordinate _destination;
        private readonly Board _board;

        public BoardCoordinate Origin
        {
            get { return _origin; }
        }

        public BoardCoordinate Destination
        {
            get { return _destination; }
        }

        public Board Board
        {
            get { return _board; }
        }

        public MoveAttempt(Board board, BoardCoordinate origin, BoardCoordinate destination)
        {
            _board = board;
            _origin = origin;
            _destination = destination;
        }
    }
}
