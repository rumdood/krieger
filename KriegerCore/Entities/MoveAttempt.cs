using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krieger
{
    public class MoveAttempt
    {
        private readonly BoardCoordinate _origin;
        private readonly BoardCoordinate _destination;

        public MoveAttempt(BoardCoordinate origin, BoardCoordinate destination)
        {
            _origin = origin;
            _destination = destination;
        }

        private IEnumerable<BoardCoordinate> getHorizontalSpacesForMove()
        {
            var biggerValue = Math.Max(_origin.XCoordinate, _destination.XCoordinate);
            var smallerValue = Math.Min(_origin.XCoordinate, _destination.XCoordinate);

            for (int i = smallerValue + 1; i < biggerValue; i++)
            {
                yield return new BoardCoordinate(i, _origin.YCoordinate);
            }
        }

        private IEnumerable<BoardCoordinate> getVerticalSpacesForMove()
        {
            var biggerValue = Math.Max(_origin.YCoordinate, _destination.YCoordinate);
            var smallerValue = Math.Min(_origin.YCoordinate, _destination.YCoordinate);

            for (int i = smallerValue + 1; i < biggerValue; i++)
            {
                yield return new BoardCoordinate(_origin.XCoordinate, i);
            }
        }

        private IEnumerable<BoardCoordinate> getDiagonalSpacesForMove()
        {
            var distance = Math.Abs(_origin.XCoordinate - _destination.XCoordinate);
            var xDirection = (_destination.XCoordinate - _origin.XCoordinate) / distance;
            var yDirection = (_destination.YCoordinate - _origin.YCoordinate) / distance;

            for (int i = 1; i < distance; i++)
            {
                yield return new BoardCoordinate(_origin.XCoordinate + (i * xDirection), _origin.YCoordinate + (i * yDirection));
            }
        }

        public IEnumerable<BoardCoordinate> GetSpacesForMove()
        {
            if (_origin.XCoordinate != _destination.XCoordinate && _origin.YCoordinate == _destination.YCoordinate)
            {
                return getHorizontalSpacesForMove();
            }
            else if (_origin.XCoordinate == _destination.XCoordinate && _origin.YCoordinate != _destination.YCoordinate)
            {
                return getVerticalSpacesForMove();
            }
            else
            {
                return getDiagonalSpacesForMove();
            }
        }
    }
}
