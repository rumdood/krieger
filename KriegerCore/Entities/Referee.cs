using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krieger.Entities
{
    public class Referee : IReferee
    {
        private readonly ICommandHandler<MoveAttempt> _moveHandler;

        public Referee(ICommandHandler<MoveAttempt> moveHandler)
        {
            _moveHandler = moveHandler;
        }

        public MoveResult TryMove(MoveAttempt attempt)
        {
            _moveHandler.Handle(attempt);
            return _moveHandler.Result as MoveResult;
        }
    }
}
