using Tacta.EventStore.Domain;

namespace Tactify.Core.Tickets.ValueObjects
{
    public sealed class Estimation : ValueObject
    {
        public int NumberOfUnits { get; }
        public MeasurementUnit MeasurementUnit { get; }

        public Estimation(int numberOfUnits, MeasurementUnit measurementUnit)
        {
            NumberOfUnits = numberOfUnits;
            MeasurementUnit = measurementUnit;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return NumberOfUnits;
            yield return MeasurementUnit;
        }
    }
}
