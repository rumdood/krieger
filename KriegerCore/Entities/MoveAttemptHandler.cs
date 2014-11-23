using System;
using System.Collections.Generic;
using System.Linq;

namespace Krieger
{
    public class MoveAttemptHandler : ICommandHandler<MoveAttempt>
    {
        private MoveAttempt _attempt;
        private MoveResult _result;

        public object Result
        {
            get { return _result; }
        }

        private bool isMovePossible()
        {
            var piece = _attempt.Board.GetPiece(_attempt.Origin);
            var legalMoves = piece.GetLegalMovesFromCoordinate(_attempt.Origin, _attempt.Board.BoardSize);

            if (!legalMoves.Contains(_attempt.Destination))
            {
                return false;
            }

            var pieceAtDesitination = _attempt.Board.GetPiece(_attempt.Destination);

            if (pieceAtDesitination != null && pieceAtDesitination.Color == piece.Color)
            {
                return false;
            }

            var path = new Path(_attempt.Origin, _attempt.Destination);

            if (!(piece is Knight) && _attempt.Board.IsPathBlocked(path))
            {
                return false;
            }

            return true;
        }

        private MoveResult simulateMoveAttempt()
        {
            var playerColor = _attempt.Board.GetPiece(_attempt.Origin).Color;
            var workspace = _attempt.Board.Clone();
            var moveResult = movePieceOnBoard(workspace, _attempt);

            if (moveResult.Success && workspace.IsPlayerInCheck(playerColor))
            {
                return MoveResult.Failed();
            }

            return moveResult;
        }

        private MoveResult movePieceOnBoard(Board board, MoveAttempt attempt)
        {
            MoveResult result = MoveResult.Succeeded();
            var movingPiece = board.GetPiece(attempt.Origin);
            var capturedPiece = board.GetPiece(attempt.Destination);

            board.RemovePiece(attempt.Origin);

            if (capturedPiece != null)
            {
                board.RemovePiece(attempt.Destination);
                result = MoveResult.Captured(capturedPiece);
            }

            board.AddPiece(movingPiece, attempt.Destination);
            return result;
        }

        public void Handle(MoveAttempt attempt)
        {
            this._attempt = attempt;

            if (!isMovePossible())
            {
                _result = MoveResult.Failed();
            }
            else
            {
                if (simulateMoveAttempt() == MoveResult.Failed())
                {
                    _result = MoveResult.Failed();
                }
                else
                {
                    _result = movePieceOnBoard(_attempt.Board, _attempt);
                }
            }
        }
    }
}
