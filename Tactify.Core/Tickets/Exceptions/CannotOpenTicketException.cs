namespace Tactify.Core.Tickets.Exceptions
{
    public sealed class CannotOpenTicketException : Exception
    {
        public CannotOpenTicketException(string message) : base(message)
        {

        }
    }
}
