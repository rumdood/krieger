using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krieger
{
    public abstract class Piece
    {
        protected int _moveLimit = 0;
        public bool HasMoved { get; set; }
        public PlayerColor Color { get; private set; }

        public Piece(PlayerColor color)
        {
            this.Color = color;
        }

        protected IEnumerable<BoardCoordinate> getDiagonalMovesFromCoordinate(BoardCoordinate origin, int maxDistance)
        {
            var coordinates = Enumerable.Range(1, maxDistance);
            return coordinates.SelectMany(c => buildDiagonalMovesEnumeration(origin, c));
        }

        protected IEnumerable<BoardCoordinate> buildDiagonalMovesEnumeration(BoardCoordinate origin, int distance)
        {
            yield return new BoardCoordinate(origin.XCoordinate + distance, origin.YCoordinate + distance);
            yield return new BoardCoordinate(origin.XCoordinate - distance, origin.YCoordinate + distance);
            yield return new BoardCoordinate(origin.XCoordinate + distance, origin.YCoordinate - distance);
            yield return new BoardCoordinate(origin.XCoordinate - distance, origin.YCoordinate - distance);
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

        public abstract IEnumerable<BoardCoordinate> GetLegalMovesFromCoordinate(BoardCoordinate origin, int boardSize);
    }
}
