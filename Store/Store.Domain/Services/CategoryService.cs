using AutoMapper;
using Framework.Core.Events;
using Framework.Core.Events.External;
using Store.Domain.Dtos;
using Store.Domain.Entities;
using Store.Domain.EventEvelopes.Category;
using Store.Domain.Repositories;

namespace Store.Domain.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategory();
        Task<CategoryDto> GetCategoryById(Guid id);
        Task<CategoryDto> AddCategory(CategoryInputDto dto);
        Task<bool> Update(CategoryInputDto dto);
        Task<bool> ChangeStatusCategory(CategoryStatusDto dto);
    }
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly IExternalEventProducer _externalEventProducer;

        public CategoryService(ICategoryRepository repository, IMapper mapper, IExternalEventProducer externalEventProducer)
        {
            _repository = repository;
            _mapper = mapper;
            _externalEventProducer = externalEventProducer;
        }

        public async Task<bool> ChangeStatusCategory(CategoryStatusDto dto)
        {
            try
            {
                if (dto.Id != new Guid())
                {
                    var existEntity = await _repository.GetById(dto.Id);
                    if (existEntity != null)
                    {
                        var entity = _mapper.Map<CategoryStatusDto, CategoryEntity>(dto, existEntity);
                        await _repository.Update(entity);
                        var result = await _repository.SaveChangesAsync();
                        if (result > 0)
                        {
                            var externalEvent = new EventEnvelope<CategoryStatusChanged>(
                                CategoryStatusChanged.Create(
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

        public async Task<CategoryDto> AddCategory(CategoryInputDto dto)
        {
            try
            {
                if (dto != null)
                {
                    var entity = await _repository.Add(_mapper.Map<CategoryEntity>(dto));
                    var result = await _repository.SaveChangesAsync();

                    if(result > 0 )
                    {
                        var externalEvent = new EventEnvelope<CategoryCreated>(
                            CategoryCreated.Create(
                            entity.Id,
                            entity.Name,
                            entity.Description,
                            entity.Status));
                        await _externalEventProducer.Publish(externalEvent, new CancellationToken());
                        return _mapper.Map<CategoryDto>(entity);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Inner Error: {e.InnerException}");
            }
            return new CategoryDto();
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategory()
        {
            try
            {
                var result = _mapper.Map<IEnumerable<CategoryDto>>(await _repository.GetAll());
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Inner Error: {e.InnerException}");
            }
            return null;
        }

        public async Task<CategoryDto> GetCategoryById(Guid id)
        {
            return _mapper.Map<CategoryDto>(await _repository.GetById(id));
        }

        public async Task<bool> Update(CategoryInputDto dto)
        {
            try
            {
                if (dto.Id != new Guid())
                {
                    var existEntity = await _repository.GetById((Guid)dto.Id);
                    if (existEntity != null)
                    {
                        var entity = _mapper.Map<CategoryInputDto, CategoryEntity>(dto, existEntity);
                        await _repository.Update(entity);
                        var result = await _repository.SaveChangesAsync();
                        if (result > 0)
                        {
                            var externalEvent = new EventEnvelope<CategoryUpdated>(
                                CategoryUpdated.Create(
                                entity.Id,
                                entity.Name,
                                entity.Description
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
