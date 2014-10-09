using System;
using System.Collections.Generic;

namespace Krieger
{
    public class Game
    {
        private readonly List<Player> _players;
        private readonly List<Board> _boards;

        public Game()
        {
            _players = new List<Player>();
            _boards = new List<Board>();
        }
    }
}
