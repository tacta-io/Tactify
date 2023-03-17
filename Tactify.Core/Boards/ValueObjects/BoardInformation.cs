using Tacta.EventStore.Domain;

namespace Tactify.Core.Boards.ValueObjects
{
    public sealed class BoardInformation : ValueObject
    {
        public string Identifier { get; }
        public string Description { get; }

        public BoardInformation(string identifier, string description)
        {
            if (string.IsNullOrWhiteSpace(identifier)) throw new Exception("Board needs to have unique identifier");
            if (string.IsNullOrWhiteSpace(description)) throw new Exception("Board description is mandatory");

            Identifier = identifier;
            Description = description;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Identifier;
            yield return Description;
        }
    }
}
