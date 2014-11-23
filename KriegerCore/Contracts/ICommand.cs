using System;

namespace Krieger
{
    public interface ICommandHandler<T>
    {
        object Result { get; }
        void Handle(T command);
    }
}
