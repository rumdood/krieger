using System;
using System.Linq;

namespace Krieger.Entities
{
    public class Referee : IReferee
    {
        public MoveResult MovePiece(Board board, BoardCoordinate start, BoardCoordinate end)
        {
            var piece = board.GetPiece(start);
            var legalMoves = piece.GetLegalMovesFromCoordinate(start, board.BoardSize);

            if (!legalMoves.Contains(end))
            {
                return MoveResult.No;
            }

            var pieceAtDesitination = board.GetPiece(end);

            if (pieceAtDesitination != null && pieceAtDesitination.Color == piece.Color)
            {
                return MoveResult.No;
            }

            // vertical move
            if (start.XCoordinate == end.XCoordinate && start.YCoordinate != end.YCoordinate)
            {
                if (!isValidVerticalMove(board, start, end))
                {
                    return MoveResult.No;
                }
            }

            // horizontal move
            if (start.XCoordinate != end.XCoordinate && start.YCoordinate == end.YCoordinate)
            {
                if (!isValidHorizontalMove(board, start, end))
                {
                    return MoveResult.No;
                }
            }

            // diagonal move
            if (start.XCoordinate != end.XCoordinate && start.YCoordinate != end.YCoordinate 
                && !(piece is Knight))
            {
                if (!isValidDiagonalMove(board, start, end))
                {
                    return MoveResult.No;
                }
            }

            if (pieceAtDesitination != null)
            {
                board.RemovePiece(end);
                board.RemovePiece(start);
                board.AddPiece(piece, end);
                return MoveResult.Capture;
            }

            board.RemovePiece(start);
            board.AddPiece(piece, end);
            return MoveResult.Yes;
        }

        protected bool isValidHorizontalMove(Board board, BoardCoordinate start, BoardCoordinate end)
        {
            var biggerValue = Math.Max(start.XCoordinate, end.XCoordinate);
            var smallerValue = Math.Min(start.XCoordinate, end.XCoordinate);

            for (int i = smallerValue + 1; i < biggerValue; i++)
            {
                var targetCoordiante = new BoardCoordinate(i, start.YCoordinate);
                if (board.GetPiece(targetCoordiante) != null)
                {
                    return false;
                }
            }

            return true;
        }

        protected bool isValidVerticalMove(Board board, BoardCoordinate start, BoardCoordinate end)
        {
            var biggerValue = Math.Max(start.YCoordinate, end.YCoordinate);
            var smallerValue = Math.Min(start.YCoordinate, end.YCoordinate);

            for (int i = smallerValue + 1; i < biggerValue; i++)
            {
                var targetCoordiante = new BoardCoordinate(start.XCoordinate, i);
                if (board.GetPiece(targetCoordiante) != null)
                {
                    return false;
                }
            }

            return true;
        }

        protected bool isValidDiagonalMove(Board board, BoardCoordinate start, BoardCoordinate end)
        {
            var biggerValue = Math.Max(start.YCoordinate, end.YCoordinate);
            var smallerValue = Math.Min(start.YCoordinate, end.YCoordinate);

            for (int i = smallerValue + 1; i < biggerValue; i++)
            {
                var targetCoordinate = new BoardCoordinate(i, i);
                if (board.GetPiece(targetCoordinate) != null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
