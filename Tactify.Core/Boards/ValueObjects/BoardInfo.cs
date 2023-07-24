using Tacta.EventStore.Domain;

namespace Tactify.Core.Boards.ValueObjects
{
    public sealed class BoardInfo : ValueObject
    {
        public string Name { get; }

        public string Description { get; }

        public string CreatedBy { get; }

        public BoardInfo(string name, string description, string createdBy)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Board needs to have unique name.");

            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Board description is mandatory.");

            Name = name;
            Description = description;
            CreatedBy = createdBy;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return Description;
            yield return CreatedBy; 
        }
    }
}
