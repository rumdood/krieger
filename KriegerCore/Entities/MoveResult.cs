using System;
using Tarantino.Core.Commons.Model.Enumerations;

namespace Krieger
{
    public class MoveResultType : Enumeration
    {
        public static readonly MoveResultType No = new MoveResultType(0, "No");
        public static readonly MoveResultType Yes = new MoveResultType(1, "Yes");

        private MoveResultType() { }
        private MoveResultType(int value, string displayName) : base(value, displayName) { } 
    }
}
