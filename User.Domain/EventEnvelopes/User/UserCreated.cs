using Framework.Core.Enums;

namespace User.Domain.EventEnvelopes.User
{
    public record UserCreated( 
        Guid Id,
        string UserName,
        string FirstName,
        string LastName,
        string Email,
        RecordStatusEnum status
     )
    {
        public static UserCreated Created(Guid id,
            string userName, string firstName, string lastName, string email, RecordStatusEnum status)
                => new(id, userName, firstName,lastName, email, status);
    }
}
