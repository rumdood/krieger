using System;
using System.Collections.Generic;
using System.Linq;

namespace Krieger
{
    public class Board : IBoard
    {
        private const int MinimumSize = 1;
        private const int DefaultBoardSize = 8;

        private readonly Dictionary<BoardCoordinate, Piece> _pieces = new Dictionary<BoardCoordinate, Piece>();

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

        protected MoveResult tryMovePiece(Piece piece, BoardCoordinate start, BoardCoordinate end)
        {
            var legalMoves = piece.GetLegalMovesFromCoordinate(start, BoardSize);

            if (!legalMoves.Contains(end))
            {
                return MoveResult.No;
            }

            var pieceAtDesitination = GetPiece(end);

            if (pieceAtDesitination != null && pieceAtDesitination.Color == piece.Color)
            {
                return MoveResult.No;
            }

            if (isMoveBlocked(start, end) && !(piece is Knight))
            {
                return MoveResult.No;
            }

            return MoveResult.Yes;
        }

        public MoveResult MovePiece(BoardCoordinate start, BoardCoordinate end)
        {
            var piece = GetPiece(start);

            if (piece == null)
            {
                return MoveResult.No;
            }

            RemovePiece(start); // pick up the piece, we'll put it back if we have a problem

            var moveResult = tryMovePiece(piece, start, end);

            if (moveResult == MoveResult.No)
            {
                AddPiece(piece, start); // put it back where we found it
                return MoveResult.No;
            }

            var capturedPiece = GetPiece(end);
            if (capturedPiece != null)
            {
                RemovePiece(end); // caputre the piece at the destination
                moveResult = MoveResult.Capture;
            }

            AddPiece(piece, end); // move the piece

            if (IsPlayerInCheck(piece.Color)) // you can't leave yourself in check, put it all back
            {
                moveResult = MoveResult.No;
                RemovePiece(end);
                AddPiece(piece, start);

                if (capturedPiece != null)
                {
                    AddPiece(capturedPiece, end);
                }
            }

            return moveResult;
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
