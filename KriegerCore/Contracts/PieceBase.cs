using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krieger
{
    public abstract class PieceBase
    {
        public PlayerColor BelongsTo { get; private set; }

        public PieceBase(PlayerColor owner)
        {
            BelongsTo = owner;
        }
    }
}
