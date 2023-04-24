namespace Tactify.Core.ReadModels.TicketReadModels
{
    public sealed class TicketReadModel : ReadModel
    {
        public string TicketId { get; set; }

        public string? SprintId { get; set; }

        public string BoardId { get; set; }

        public string Description { get; set; }

        public string? Assignee { get; set; }

        public int? Estimation { get; set; }

        public bool IsClosed { get; set; }
    }
}
