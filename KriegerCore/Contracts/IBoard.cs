using System;

namespace Krieger
{
    public interface IBoard
    {
        void AddPiece(Piece piece, BoardCoordinate location);
        void RemovePiece(BoardCoordinate location);
        Piece GetPiece(BoardCoordinate location);
    }
}
