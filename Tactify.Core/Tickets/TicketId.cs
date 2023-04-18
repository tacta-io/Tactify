﻿using Tacta.EventStore.Domain;

namespace Tactify.Core.Tickets
{
    public sealed class TicketId : EntityId
    {
        public string BoardName { get; private set; }

        public int TicketNumber { get; private set; }

        public TicketId(string boardName, int ticketNumber)
        {
            BoardName = boardName;

            TicketNumber = ticketNumber;
        }

        public static TicketId Identity(string ticketId)
        {
            var values = ticketId.Split("-");

            return new TicketId(values[1], int.Parse(values[2]));
        }

        public override string ToString()
        {
            return $"Ticket-{BoardName}-{TicketNumber}";
        }
    }
}
