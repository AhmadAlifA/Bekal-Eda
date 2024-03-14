using User.Domain.Dtos;
using User.Domain.Services;

namespace User.GraphQL.Schema.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class UserQuery
    {
        private readonly IUserService _service;

        public UserQuery(IUserService service)
        {
            _service = service;
        }

        [UsePaging]
        public async Task<IEnumerable<UserDto>> GetAllUser()
        {
            return await _service.GetAllUser();
        }
    }
}
