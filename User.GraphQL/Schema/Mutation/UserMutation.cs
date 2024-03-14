using User.Domain.Dtos;
using User.Domain.Services;

namespace User.GraphQL.Schema.Mutation
{
    [ExtendObjectType(typeof(Mutation))]
    public class UserMutation
    {
        private readonly IUserService _service;
        public UserMutation(IUserService service)
        {
            _service = service;
        }
        public async Task<UserDto> AddUserAsync(UserDto dto)
        {
            return await _service.AddUser(dto);
        }
    }
}
