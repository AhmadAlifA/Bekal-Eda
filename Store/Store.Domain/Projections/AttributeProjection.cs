using Framework.Core.Enums;
using Framework.Core.Events;
using Store.Domain.Entities;

namespace Store.Domain.Projections
{
    public record AttributeCreated
    (
        Guid? Id,
        AttributeTypeEnum Type,
        string Unit,
        RecordStatusEnum Status
    );

    public record AttributeUpdated(
        Guid? Id,
        AttributeTypeEnum Type,
        string Unit
    );

    public record AttributeStatusChanged(
        Guid? Id,
        RecordStatusEnum Status
    );

    public class AttributeProjection
    {
        public static bool Handle(EventEnvelope<AttributeCreated> eventEnvelope)
        {
            var (id, type, unit, status) = eventEnvelope.Data;
            using (var context = new StoreDbContext(StoreDbContext.OnConfigure()))
            {
                AttributeEntity entity = new AttributeEntity()
                {
                    Id = (Guid)id,
                    Type = type,
                    Unit = unit,
                    Status = status
                };
                context.Add(entity);
                context.SaveChanges();
            }
            return true;
        }

        public static bool Handle(EventEnvelope<AttributeUpdated> eventEnvelope)
        {
            try
            {
                var (id, type, unit) = eventEnvelope.Data;
                using (var context = new StoreDbContext(StoreDbContext.OnConfigure()))
                {
                    AttributeEntity? entity = context.Set<AttributeEntity>().Find(id);
                    entity.Type = type;
                    entity.Unit = unit;
                    context.Update(entity);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Inner Error: {e.InnerException}");
            }

            return true;
        }

        public static bool Handle(EventEnvelope<AttributeStatusChanged> eventEnvelope)
        {
            try
            {
                var (id, status) = eventEnvelope.Data;
                using (var context = new StoreDbContext(StoreDbContext.OnConfigure()))
                {
                    AttributeEntity? entity = context.Set<AttributeEntity>().Find(id);
                    entity.Status = status;
                    context.Update(entity);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Inner Error: {e.InnerException}");
            }

            return true;
        }
    }
}
