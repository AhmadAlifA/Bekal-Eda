using Framework.Core.Enums;

namespace Store.Domain.EventEvelopes.Category
{
    public record CategoryCreated (
        Guid Id,
        string Name,
        string Description,
        RecordStatusEnum Status
    )
    {
        public static CategoryCreated Create(Guid id,
            string name,
            string description,
            RecordStatusEnum status) => new(id, name, description, status);
    }

    public record CategoryUpdated(
        Guid Id,
        string Name,
        string Description
        )
    {
        public static CategoryUpdated Create(Guid id,
            string name,
            string description) => new(id, name, description);
    }

    public record CategoryStatusChanged(
        Guid Id,
        RecordStatusEnum Status
        )
    {
        public static CategoryStatusChanged Create(Guid id,
            RecordStatusEnum status) => new(id, status);
    }


}
