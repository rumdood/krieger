namespace Krieger;

public class InvalidMoveException : Exception
{
    public InvalidMoveException(string message) : base(message) { }
}

public class InvalidPlacementException : Exception
{
    public InvalidPlacementException(string message) : base(message) { }
}

public class InvalidLocationException : Exception
{
    public InvalidLocationException(string message) : base(message) { }
}