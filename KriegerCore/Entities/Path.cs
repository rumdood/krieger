using System;
using System.Collections.Generic;
using System.Linq;

namespace Krieger
{
    public class Path : IPath
    {
        private readonly BoardCoordinate _origin;
        private readonly BoardCoordinate _destination;

        public Path(BoardCoordinate start, BoardCoordinate end)
        {
            _origin = start;
            _destination = end;
        }

        private IEnumerable<BoardCoordinate> getHorizontalSpaces()
        {
            var biggerValue = Math.Max(_origin.XCoordinate, _destination.XCoordinate);
            var smallerValue = Math.Min(_origin.XCoordinate, _destination.XCoordinate);

            for (int i = smallerValue + 1; i < biggerValue; i++)
            {
                yield return new BoardCoordinate(i, _origin.YCoordinate);
            }
        }

        private IEnumerable<BoardCoordinate> getVerticalSpaces()
        {
            var biggerValue = Math.Max(_origin.YCoordinate, _destination.YCoordinate);
            var smallerValue = Math.Min(_origin.YCoordinate, _destination.YCoordinate);

            for (int i = smallerValue + 1; i < biggerValue; i++)
            {
                yield return new BoardCoordinate(_origin.XCoordinate, i);
            }
        }

        private IEnumerable<BoardCoordinate> getDiagonalSpaces()
        {
            var distance = Math.Abs(_origin.XCoordinate - _destination.XCoordinate);
            var xDirection = (_destination.XCoordinate - _origin.XCoordinate) / distance;
            var yDirection = (_destination.YCoordinate - _origin.YCoordinate) / distance;

            for (int i = 1; i < distance; i++)
            {
                yield return new BoardCoordinate(_origin.XCoordinate + (i * xDirection), _origin.YCoordinate + (i * yDirection));
            }
        }

        public IEnumerable<BoardCoordinate> GetSpaces()
        {
            if (_origin.XCoordinate != _destination.XCoordinate && _origin.YCoordinate == _destination.YCoordinate)
            {
                return getHorizontalSpaces();
            }
            else if (_origin.XCoordinate == _destination.XCoordinate && _origin.YCoordinate != _destination.YCoordinate)
            {
                return getVerticalSpaces();
            }
            else
            {
                return getDiagonalSpaces();
            }
        }
    }
}
