using System;
using System.Collections.Generic;

namespace Krieger
{
    public interface IPath
    {
        IEnumerable<BoardCoordinate> GetSpaces();
    }
}
