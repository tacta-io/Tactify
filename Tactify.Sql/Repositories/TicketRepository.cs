using Dapper;
using Tacta.Connection;
using Tacta.EventStore.Domain;
using Tacta.EventStore.Repository;
using Tacta.EventStore.Repository.Models;
using Tactify.Core.Tickets;
using Tactify.Core.Tickets.Exceptions;
using Tactify.Core.Tickets.Repositories;

namespace Tactify.Sql.Repositories
{
    public sealed class TicketRepository : ITicketRepository
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        private readonly IConnectionFactory _sqlConnectionFactory;


        public TicketRepository(IEventStoreRepository eventStoreRepository, IConnectionFactory sqlConnectionFactory)
        {
            _eventStoreRepository = eventStoreRepository;
            _sqlConnectionFactory = sqlConnectionFactory;
        }


        public async Task<Ticket> GetAsync(TicketId ticketId)
        {
            var eventStoreRecords = await _eventStoreRepository.GetAsync<DomainEvent>(ticketId.ToString()).ConfigureAwait(false);

            eventStoreRecords.ToList().ForEach(x => x.Event.WithVersionAndSequence(x.Version, x.Sequence));

            var domainEvents = eventStoreRecords.Select(x => x.Event).ToList().AsReadOnly();

            if (!domainEvents.Any()) throw new CannotFindTicketException($"Ticket {ticketId} not found.");

            return new Ticket(domainEvents);
        }       

        public async Task SaveAsync(Ticket ticket)
        {
            var aggregateRecord = new AggregateRecord(ticket.Id.ToString(), ticket.GetType().Name, ticket.Version);

            var eventRecords = ticket.DomainEvents
                .Select(@event => new EventRecord<IDomainEvent>(((DomainEvent)@event).Id, @event.CreatedAt, @event)).ToList()
                .AsReadOnly();

            await _eventStoreRepository.SaveAsync(aggregateRecord, eventRecords).ConfigureAwait(false);
        }

        public async Task<int> GetNextTicketNumberAsync()
        {
            const string insert = "INSERT INTO [dbo].[TicketNumberLookup] DEFAULT VALUES;";

            const string select = "SELECT CAST (SCOPE_IDENTITY() AS INT);";

            await using var connection = _sqlConnectionFactory.Connection();

            return await connection.QueryFirstAsync<int>($"{insert} {select}").ConfigureAwait(false);
        }
    }
}
