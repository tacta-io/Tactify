namespace Tactify.Core.Boards.Exceptions
{  
    public sealed class CannotEndActiveSprintException : Exception
    {
        public CannotEndActiveSprintException(string message) : base(message)
        {

        }
    }
}
