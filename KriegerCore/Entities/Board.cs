using System;
using System.Collections.Generic;
using System.Linq;

namespace Krieger
{
    public class Board : IBoard
    {
        public const int MaximumSize = 8;
        public const int MinimumSize = 1;

        private readonly Dictionary<BoardCoordinate, IPiece> _pieces = new Dictionary<BoardCoordinate, IPiece>();

        public Board()
        {
        }

        public void AddPiece(IPiece piece, BoardCoordinate location)
        {
            if (location.IsValidForBoard(MaximumSize))
            {
                if (!_pieces.ContainsKey(location))
                {
                    _pieces.Add(location, piece);
                }
                else
                {
                    throw new InvalidOperationException("BoardCoordinate Is Occupied");
                }
            }
            else
            {
                throw new InvalidOperationException("BoardCoordinate Is Out Of Range");
            }
        }


        public IPiece GetPiece(BoardCoordinate location)
        {
            if (_pieces.ContainsKey(location))
            {
                return _pieces[location];
            }
            else
            {
                return null;
            }
        }
    }
}
