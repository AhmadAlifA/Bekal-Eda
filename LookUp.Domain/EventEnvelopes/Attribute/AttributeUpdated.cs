using Framework.Core.Enums;

namespace LookUp.Domain.EventEnvelopes.Attribute
{
    public record AttributeUpdated(
        Guid? Id,
        AttributeTypeEnum Type,
        string Unit
     )
    {
        public static AttributeUpdated Create(Guid? id, AttributeTypeEnum type, string unit)
        => new(id, type, unit);
    }
}
