using Tacta.EventStore.Domain;
using Tacta.EventStore.Repository;
using Tactify.Core.Boards;
using Tactify.Core.Boards.Exceptions;
using Tactify.Core.Boards.Repositories;

namespace Tactify.Sql.Repositories
{
    public sealed class BoardRepository : IBoardRepository
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        public BoardRepository(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<Board> GetAsync(BoardId boardId)
        {
            var eventStoreRecords = await _eventStoreRepository.GetAsync<DomainEvent>(boardId.ToString()).ConfigureAwait(false);

            eventStoreRecords.ToList().ForEach(x => x.Event.WithVersionAndSequence(x.Version, x.Sequence));

            var domainEvents = eventStoreRecords.Select(x => x.Event).ToList().AsReadOnly();

            if (!domainEvents.Any()) throw new CannotFindBoardException($"Board {boardId} not found.");

            return new Board(domainEvents);
        }

        public async Task SaveAsync(Board board)
        {
            var aggregateRecord = new AggregateRecord(board.Id.ToString(), board.GetType().Name, board.Version);

            var eventRecords = board.DomainEvents
                .Select(@event => new EventRecord<IDomainEvent>(((DomainEvent)@event).Id, @event.CreatedAt, @event)).ToList()
                .AsReadOnly();

            await _eventStoreRepository.SaveAsync(aggregateRecord, eventRecords).ConfigureAwait(false);
        }
    }
}
