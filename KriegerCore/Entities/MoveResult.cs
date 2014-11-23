using System;
using System.Collections.Generic;
using System.Linq;

namespace Krieger
{
    public class MoveResult
    {
        private readonly bool _success = false;
        private readonly Piece _captured;

        public bool Success
        {
            get { return _success; }
        }

        public Piece CapturedPiece
        {
            get { return _captured; }
        }

        private MoveResult(bool success, Piece captured = null)
        {
            _success = success;
            _captured = captured;
        }

        public static MoveResult Failed()
        {
            return new MoveResult(false);
        }

        public static MoveResult Succeeded()
        {
            return new MoveResult(true);
        }

        public static MoveResult Captured(Piece captured)
        {
            return new MoveResult(true, captured);
        }

        public override bool Equals(object obj)
        {
            var compareResult = obj as MoveResult;

            if (compareResult == null)
            {
                return false;
            }

            return compareResult.Success == this.Success
                && compareResult.CapturedPiece == this.CapturedPiece;
        }

        public static bool operator ==(MoveResult a, MoveResult b)
        {
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            if ((object)a == null ^ (object)b == null)
            {
                return false;
            }

            return a.Success == b.Success && a.CapturedPiece == b.CapturedPiece;
        }

        public static bool operator !=(MoveResult a, MoveResult b)
        {
            return !(a == b);
        }
    }
}
