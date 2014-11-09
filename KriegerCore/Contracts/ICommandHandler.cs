using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krieger
{
    public interface ICommandHandler<T>
    {
        T Result { get; }
        void Execute();
    }
}
