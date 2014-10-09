using System;
using System.Collections.Generic;

namespace Krieger
{
    public struct BoardCoordinate
    {
        private int _x;
        private int _y;

        public int XCoordinate { get { return _x; } }
        public int YCoordinate { get { return _y; } }

        public BoardCoordinate(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public bool IsValidForBoard(int maxBoardSize)
        {
            return (_x > 0 
                && _y > 0
                && _x <= maxBoardSize 
                && _y <= maxBoardSize);
        }
    }
}
