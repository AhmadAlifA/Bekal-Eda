using Framework.Core.Enums;
using Framework.Core.Events;
using Order.Domain.Entities;

namespace Order.Domain.Projection
{
    public record UserCreated(
        Guid Id,
        string FirstName,
        string LastName,
        string Email
    );
    public class UserProjection
    {
        public static bool Handle(EventEnvelope<UserCreated> eventEnvelope)
        {
            var (id, firstName, lastName, email) = eventEnvelope.Data;
            using (var context = new OrderDbContext(OrderDbContext.OnConfigure()))
            {
                UserEntity entity = new UserEntity()
                {
                    Id = (Guid)id,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email
                };
                context.Add(entity);
                context.SaveChanges();
            }
            return true;
        }
    }
}
