using System;

namespace Krieger
{
    public interface IBoard
    {
        void AddPiece(Piece piece, BoardCoordinate location);
        void RemovePiece(BoardCoordinate location);
        MoveResult MovePiece(BoardCoordinate start, BoardCoordinate end);
        Piece GetPiece(BoardCoordinate location);
        bool IsPlayerInCheck(PlayerColor color);
    }
}
