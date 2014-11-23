using System;
using System.Collections.Generic;
using System.Linq;

namespace Krieger
{
    public class Board : IBoard
    {
        private const int MinimumSize = 1;
        private const int DefaultBoardSize = 8;

        private Dictionary<BoardCoordinate, Piece> _pieces = new Dictionary<BoardCoordinate, Piece>();

        public int BoardSize { get; private set; }

        public Board(int boardSize = DefaultBoardSize)
        {
            if (boardSize < MinimumSize)
            {
                throw new ArgumentException("Board Size Must Be Greater Than 0");
            }

            BoardSize = boardSize;
        }

        public bool IsPathBlocked(Path path)
        {
            foreach (var square in path.GetSpaces())
            {
                if (GetPiece(square) != null)
                {
                    return true;
                }
            }

            return false;
        }

        protected BoardCoordinate getKingLocationByColor(PlayerColor color)
        {
            var keyValuePair = _pieces.Where(x => x.Value is King && x.Value.Color == color).FirstOrDefault();
            return keyValuePair.Key;
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

        public bool IsPlayerInCheck(PlayerColor color)
        {
            var kingLocation = getKingLocationByColor(color);
            var enemyPieces = _pieces.Where(x => x.Value.Color != color);

            foreach (var pieceAndLocation in enemyPieces)
            {
                var piece = pieceAndLocation.Value;
                var location = pieceAndLocation.Key;

                if (piece.GetLegalMovesFromCoordinate(location, BoardSize).Contains(kingLocation))
                {
                    var path = new Path(location, kingLocation);
                    if (!IsPathBlocked(path))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public Board Clone()
        {
            var clone = new Board(this.BoardSize);

            foreach (var key in _pieces.Keys)
            {
                clone.AddPiece(_pieces[key], key);
            }

            return clone;
        }
    }
}
