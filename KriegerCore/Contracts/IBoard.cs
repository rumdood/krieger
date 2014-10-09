using System;

namespace Krieger
{
    public interface IBoard
    {
        void AddPiece(IPiece piece, BoardCoordinate location);
        IPiece GetPiece(BoardCoordinate location);
    }
}
