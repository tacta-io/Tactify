namespace Tactify.Core.Tickets.Exceptions
{
    public sealed class CannotCloseTicketException : Exception
    {
        public CannotCloseTicketException(string message) : base(message)
        {

        }
    }
}
