using Tacta.EventStore.Domain;
using Tactify.Core.Tickets.ValueObjects;

namespace Tactify.Core.Tickets.DomainEvents
{
    public sealed class TicketEstimated : DomainEvent
    {
        public int NumberOfUnits { get; }
        public MeasurementUnit MeasurementUnit { get; }

        public TicketEstimated(string aggregateId, int numberOfUnits, MeasurementUnit measurementUnit) : base(aggregateId)
        {
            NumberOfUnits = numberOfUnits;
            MeasurementUnit = measurementUnit;
        }
    }
}
