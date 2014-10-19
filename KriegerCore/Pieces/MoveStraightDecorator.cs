using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krieger.Pieces
{
    public class MoveStraightDecorator : Piece
    {
        private readonly Piece _piece;

        public MoveStraightDecorator(PlayerColor color, Piece piece) : base(color)
        {
            _piece = piece;
        }

        public override IEnumerable<BoardCoordinate> GetLegalMovesFromCoordinate(BoardCoordinate origin, int boardSize)
        {
            return getStraightMovesFromCoordinate(origin, boardSize);
        }

        protected IEnumerable<BoardCoordinate> getStraightMovesFromCoordinate(BoardCoordinate origin, int maxDistance)
        {
            var coordinates = Enumerable.Range(1, maxDistance);
            return coordinates.SelectMany(c => buildStraightMovesEnumeration(origin, c));
        }

        protected IEnumerable<BoardCoordinate> buildStraightMovesEnumeration(BoardCoordinate origin, int distance)
        {
            yield return new BoardCoordinate(origin.XCoordinate + distance, origin.YCoordinate);
            yield return new BoardCoordinate(origin.XCoordinate - distance, origin.YCoordinate);
            yield return new BoardCoordinate(origin.XCoordinate, origin.YCoordinate + distance);
            yield return new BoardCoordinate(origin.XCoordinate, origin.YCoordinate - distance);
        }
    }
}
