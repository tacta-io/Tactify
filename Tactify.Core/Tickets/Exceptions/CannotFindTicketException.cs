namespace Tactify.Core.Tickets.Exceptions
{
    public sealed class CannotFindTicketException : Exception
    {
        public CannotFindTicketException(string message) : base(message)
        {

        }
    }
}
