using AutoMapper;
using Framework.Core.Events.External;
using Framework.Core.Events;
using LookUp.Domain.Dtos;
using LookUp.Domain.Repositories;
using LookUp.Domain.Entities;
using LookUp.Domain.EventEnvelopes.Attribute;

namespace LookUp.Domain.Services
{
    public interface IAttributeService
    {
        Task<AttributeDto> AddAttribute(AttributeDto dto);
        Task<IEnumerable<AttributeDto>> GetAllAttribute();
        Task<AttributeDto> GetAttributeById(Guid id);
        Task<bool> UpdateAttribute(AttributeExceptStatusDto dto);
        Task<bool> ChangeStatusAttribute(AttributeStatusDto dto);
    }
    public class AttributeService : IAttributeService
    {
        private IAttributeRepository _repository;
        private readonly IMapper _mapper;
        private readonly IExternalEventProducer _externalEventProducer;

        public AttributeService(IAttributeRepository repository, IMapper mapper, IExternalEventProducer externalEventProducer)
        {
            _repository = repository;
            _mapper = mapper;
            _externalEventProducer = externalEventProducer;
        }

        public async Task<AttributeDto> AddAttribute(AttributeDto dto)
        {
            try
            {
                var dtoToEntity = _mapper.Map<AttributeEntity>(dto);

                if (dto != null)
                {
                    var entity = await _repository.Add(dtoToEntity);
                    var result = await _repository.SaveChangesAsync();

                    //Event driven, Event bus
                    if (result > 0)
                    {
                        var externalEvent = new EventEnvelope<AttributeCreated>(
                            AttributeCreated.Created(
                            entity.Id,
                            entity.Type,
                            entity.Unit,
                            entity.Status));
                        await _externalEventProducer.Publish(externalEvent, new CancellationToken());
                        return _mapper.Map<AttributeDto>(entity);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            return new AttributeDto();
        }

        public async Task<IEnumerable<AttributeDto>> GetAllAttribute()
        {
            return _mapper.Map<IEnumerable<AttributeDto>>(await _repository.GetAll());
        }

        public async Task<AttributeDto> GetAttributeById(Guid id)
        {
            return _mapper.Map<AttributeDto>(await _repository.GetById(id));
        }

        public async Task<bool> UpdateAttribute(AttributeExceptStatusDto dto)
        {
            try
            {
                if (dto.Id != new Guid())
                {
                    var existEntity = await _repository.GetById((Guid)dto.Id);
                    if (existEntity != null)
                    {
                        var entity = _mapper.Map<AttributeExceptStatusDto, AttributeEntity>(dto, existEntity);
                        await _repository.Update(entity);
                        var result = await _repository.SaveChangesAsync();
                        if (result > 0)
                        {
                            var externalEvent = new EventEnvelope<AttributeUpdated>(
                                AttributeUpdated.Create(
                                entity.Id,
                                entity.Type,
                                entity.Unit
                                ));
                            await _externalEventProducer.Publish(externalEvent, new CancellationToken());

                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Inner Error: {e.InnerException}");
            }
            return false;
        }

        public async Task<bool> ChangeStatusAttribute(AttributeStatusDto dto)
        {
            try
            {
                if (dto.Id != new Guid())
                {
                    var existEntity = await _repository.GetById((Guid)dto.Id);
                    if (existEntity != null)
                    {
                        var entity = _mapper.Map<AttributeStatusDto, AttributeEntity>(dto, existEntity);
                        await _repository.Update(entity);
                        var result = await _repository.SaveChangesAsync();
                        if (result > 0)
                        {
                            var externalEvent = new EventEnvelope<AttributeStatusChanged>(
                                AttributeStatusChanged.Create(
                                entity.Id,
                                entity.Status
                                ));
                            await _externalEventProducer.Publish(externalEvent, new CancellationToken());

                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Inner Error: {e.InnerException}");
            }
            return false;
        }
    }
}
