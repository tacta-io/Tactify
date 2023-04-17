using Tacta.EventStore.Domain;

namespace Tactify.Core.Boards.ValueObjects
{
    public sealed class BoardInformation : ValueObject
    {
        public string Identifier { get; }
        public string Description { get; }
        public string CreatedBy { get; }

        public BoardInformation(string identifier, string description, string createdBy)
        {
            if (string.IsNullOrWhiteSpace(identifier)) throw new Exception("Board needs to have unique identifier");
            if (string.IsNullOrWhiteSpace(description)) throw new Exception("Board description is mandatory");

            Identifier = identifier;
            Description = description;
            CreatedBy = createdBy;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Identifier;
            yield return Description;
            yield return CreatedBy; 
        }
    }
}
