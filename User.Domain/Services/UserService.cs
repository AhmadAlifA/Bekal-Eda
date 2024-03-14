using AutoMapper;
using Framework.Auth;
using Framework.Core.Events;
using Framework.Core.Events.External;
using User.Domain.Dtos;
using User.Domain.Entities;
using User.Domain.EventEnvelopes.User;
using User.Domain.Repositories;

namespace User.Domain.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUser();
        Task<LoginDto> Login(string username, string password);
        Task<UserDto?> AddUser(UserDto dto);
    }
    public class UserService : IUserService
    {
        private IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IExternalEventProducer _externalEventProducer;

        public UserService(IUserRepository repository, IMapper mapper, IExternalEventProducer externalEventProducer)
        {
            _repository = repository;
            _mapper = mapper;
            _externalEventProducer = externalEventProducer;
        }

        public async Task<UserDto> AddUser(UserDto dto)
        {
            try
            {
                dto.Password = Encryption.HashSha256(dto.Password);
                var dtoToEntity = _mapper.Map<UserEntity>(dto);
                var entity = await _repository.Add(dtoToEntity);
                var result = await _repository.SaveChangesAsync();
            //Event driven, Event bus
            if (result > 0)
            {
                var externalEvent = new EventEnvelope<UserCreated>(
                    UserCreated.Created(
                    entity.Id,
                    entity.UserName,
                    entity.FirstName,
                    entity.LastName,
                    entity.Email,
                    entity.Status));
                await _externalEventProducer.Publish(externalEvent, new CancellationToken());
                return _mapper.Map<UserDto>(entity);
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Error: {ex.InnerException}");
            }

            return new UserDto();
        }

        public async Task<LoginDto?> Login(string username, string password)
        {
            var entity = await _repository.Login(username, password);

            if (entity != null) 
            { 
                LoginDto dto = _mapper.Map<LoginDto>(entity);
                dto.Roles.Add(entity.Type.ToString());
                return dto;
            }
            return null;
        }

        public async Task<IEnumerable<UserDto>> GetAllUser()
        {
            return _mapper.Map<IEnumerable<UserDto>>(await _repository.GetAll());
        }
    }
}
