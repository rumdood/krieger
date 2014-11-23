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

        protected bool isMoveBlocked(BoardCoordinate start, BoardCoordinate end)
        {
            var moveAttempt = new MoveAttempt(start, end);
            var spaces = moveAttempt.GetSpacesForMove();

            foreach (var square in spaces)
            {
                if (GetPiece(square) != null)
                    return true;
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

        protected bool isMovePossible(Piece piece, BoardCoordinate start, BoardCoordinate end)
        {
            var legalMoves = piece.GetLegalMovesFromCoordinate(start, BoardSize);

            if (!legalMoves.Contains(end))
            {
                return false;
            }

            var pieceAtDesitination = GetPiece(end);

            if (pieceAtDesitination != null && pieceAtDesitination.Color == piece.Color)
            {
                return false;
            }

            if (isMoveBlocked(start, end) && !(piece is Knight))
            {
                return false;
            }

            return true;
        }

        public MoveResult MovePiece(BoardCoordinate start, BoardCoordinate end)
        {
            var piece = GetPiece(start);

            if (piece == null)
            {
                return MoveResult.Failed();
            }

            RemovePiece(start); // pick up the piece, we'll put it back if we have a problem

            var moveValidated = isMovePossible(piece, start, end);

            if (!moveValidated)
            {
                AddPiece(piece, start); // put it back where we found it
                return MoveResult.Failed();
            }

            MoveResult result = MoveResult.Failed();

            var capturedPiece = GetPiece(end);
            if (capturedPiece != null)
            {
                RemovePiece(end); // caputre the piece at the destination
            }

            AddPiece(piece, end); // move the piece

            if (IsPlayerInCheck(piece.Color)) // you can't leave yourself in check, put it all back
            {
                RemovePiece(end);
                AddPiece(piece, start);

                if (capturedPiece != null)
                {
                    AddPiece(capturedPiece, end);
                }
            }
            else
            {
                if (capturedPiece != null)
                {
                    result = MoveResult.Captured(capturedPiece);
                }
                else
                {
                    result = MoveResult.Succeeded();
                }
            }

            return result;
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
                    if (!isMoveBlocked(location, kingLocation))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
