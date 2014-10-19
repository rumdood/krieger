using System;
using System.Collections.Generic;
using System.Linq;

namespace Krieger
{
    public class Board : IBoard
    {
        private const int MinimumSize = 1;

        private readonly Dictionary<BoardCoordinate, Piece> _pieces = new Dictionary<BoardCoordinate, Piece>();

        public int BoardSize { get; private set; }

        public Board(int boardSize)
        {
            BoardSize = boardSize;
        }

        public void AddPiece(Piece piece, BoardCoordinate location)
        {
            if (location.IsValidForBoard(BoardSize))
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

        public void RemovePiece(BoardCoordinate location)
        {
            _pieces.Remove(location);
        }

        public Piece GetPiece(BoardCoordinate location)
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
