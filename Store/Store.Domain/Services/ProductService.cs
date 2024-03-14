using AutoMapper;
using Framework.Core.Events;
using Framework.Core.Events.External;
using Store.Domain.Dtos;
using Store.Domain.Entities;
using Store.Domain.EventEvelopes.Category;
using Store.Domain.EventEvelopes.Product;
using Store.Domain.Repositories;

namespace Store.Domain.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProduct();
        Task<ProductDto> GetProductById(Guid id);
        Task<ProductDto> AddProduct(ProductCreateDto dto);
        Task<ProductDto> Update(ProductUpdateDto dto);
        Task<ProductDto> ChangeCategory(ProductCategoryDto dto);
        Task<ProductDto> ChangeAttribute(ProductAttributeDto dto);
        Task<ProductDto> ChangePriceVolume(ProductPriceVolumeDto dto);
        Task<ProductDto> ChangeStockSold(ProductStockSoldDto dto);
        Task<ProductDto> ProductChangeStatus(ProductStatusDto dto);
    }
    public class ProductService : IProductService
    {
        private IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IExternalEventProducer _externalEventProducer;
        public ProductService(IProductRepository repository, IMapper mapper, IExternalEventProducer externalEventProducer)
        {
            _repository = repository;
            _mapper = mapper;
            _externalEventProducer = externalEventProducer;
        }
        public async Task<ProductDto> AddProduct(ProductCreateDto dto)
        {
            try
            {
                if (dto != null)
                {
                    var entity = await _repository.Add(_mapper.Map<ProductEntity>(dto));
                    var result = await _repository.SaveChangesAsync();

                    if (result > 0)
                    {
                        var externalEvent = new EventEnvelope<ProductCreated>(
                            ProductCreated.Create(
                            entity.Id,
                            entity.CategoryId,
                            entity.AttributeId,
                            entity.Sku,
                            entity.Name,
                            entity.Description,
                            entity.Status));
                        await _externalEventProducer.Publish(externalEvent, new CancellationToken());
                        return _mapper.Map<ProductDto>(entity);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Inner Error: {e.InnerException}");
            }
            return new ProductDto();
        }

        public async Task<ProductDto> ChangeAttribute(ProductAttributeDto dto)
        {
            try
            {
                if (dto.Id != new Guid())
                {
                    var existEntity = await _repository.GetById(dto.Id);
                    if (existEntity != null)
                    {
                        var entity = _mapper.Map<ProductAttributeDto, ProductEntity>(dto, existEntity);
                        await _repository.Update(entity);
                        var result = await _repository.SaveChangesAsync();
                        if (result > 0)
                        {
                            var externalEvent = new EventEnvelope<ProductAttributeChanged>(
                                ProductAttributeChanged.Create(
                                entity.Id,
                                entity.AttributeId
                                ));
                            await _externalEventProducer.Publish(externalEvent, new CancellationToken());

                            return _mapper.Map<ProductDto>(entity);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Inner Error: {e.InnerException}");
            }
            return null;
        }

        public async Task<ProductDto> ChangeCategory(ProductCategoryDto dto)
        {
            try
            {
                if (dto.Id != new Guid())
                {
                    var existEntity = await _repository.GetById(dto.Id);
                    if (existEntity != null)
                    {
                        var entity = _mapper.Map<ProductCategoryDto, ProductEntity>(dto, existEntity);
                        await _repository.Update(entity);
                        var result = await _repository.SaveChangesAsync();
                        if (result > 0)
                        {
                            var externalEvent = new EventEnvelope<ProductCategoryChanged>(
                                ProductCategoryChanged.Create(
                                entity.Id,
                                entity.CategoryId
                                ));
                            await _externalEventProducer.Publish(externalEvent, new CancellationToken());

                            return _mapper.Map<ProductDto>(entity);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Inner Error: {e.InnerException}");
            }
            return null;
        }

        public async Task<ProductDto> ChangePriceVolume(ProductPriceVolumeDto dto)
        {
            try
            {
                if (dto.Id != new Guid())
                {
                    var existEntity = await _repository.GetById(dto.Id);
                    if (existEntity != null)
                    {
                        var entity = _mapper.Map<ProductPriceVolumeDto, ProductEntity>(dto, existEntity);
                        await _repository.Update(entity);
                        var result = await _repository.SaveChangesAsync();
                        if (result > 0)
                        {
                            var externalEvent = new EventEnvelope<ProductPriceVolumeChanged>(
                                ProductPriceVolumeChanged.Create(
                                entity.Id, entity.Price, entity.Volume
                                ));
                            await _externalEventProducer.Publish(externalEvent, new CancellationToken());

                            return _mapper.Map<ProductDto>(entity);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Inner Error: {e.InnerException}");
            }
            return null;
        }

        public async Task<ProductDto> ProductChangeStatus(ProductStatusDto dto)
        {
            try
            {
                if (dto.Id != new Guid())
                {
                    var existEntity = await _repository.GetById(dto.Id);
                    if (existEntity != null)
                    {
                        var entity = _mapper.Map<ProductStatusDto, ProductEntity>(dto, existEntity);
                        await _repository.Update(entity);
                        var result = await _repository.SaveChangesAsync();
                        if (result > 0)
                        {
                            var externalEvent = new EventEnvelope<ProductStatusChanged>(
                                ProductStatusChanged.Create(entity.Id, entity.Status));

                            await _externalEventProducer.Publish(externalEvent, new CancellationToken());

                            return _mapper.Map<ProductDto>(entity);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Error: {e.InnerException}");
            }
            return null;

        }

        public async Task<ProductDto> ChangeStockSold(ProductStockSoldDto dto)
        {
            try
            {
                if (dto.Id != new Guid())
                {
                    var existEntity = await _repository.GetById(dto.Id);
                    if (existEntity != null)
                    {
                        var entity = _mapper.Map<ProductStockSoldDto, ProductEntity>(dto, existEntity);
                        await _repository.Update(entity);
                        var result = await _repository.SaveChangesAsync();
                        if (result > 0)
                        {
                            var externalEvent = new EventEnvelope<ProductStockSoldChanged>(
                                ProductStockSoldChanged.Create(entity.Id, entity.Stock, entity.Sold));

                            await _externalEventProducer.Publish(externalEvent, new CancellationToken());

                            return _mapper.Map<ProductDto>(entity);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            return null;

        }

        public async Task<IEnumerable<ProductDto>> GetAllProduct()
        {
            try
            {
                var ents = await _repository.GetAll();
                var result = _mapper.Map<IEnumerable<ProductDto>>(ents);
                return result;
            }catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Error: {e.InnerException}");
            }
            return null;
        }

        public async Task<ProductDto> GetProductById(Guid id)
        {
            return _mapper.Map<ProductDto>(await _repository.GetById(id));
        }

        public async Task<ProductDto> Update(ProductUpdateDto dto)
        {
            try
            {
                if (dto.Id != new Guid())
                {
                    var existEntity = await _repository.GetById(dto.Id);
                    if (existEntity != null)
                    {
                        var entity = _mapper.Map<ProductUpdateDto, ProductEntity>(dto, existEntity);
                        await _repository.Update(entity);
                        var result = await _repository.SaveChangesAsync();
                        if (result > 0)
                        {
                            var externalEvent = new EventEnvelope<ProductUpdated>(
                                ProductUpdated.Create(entity.Id, entity.Sku, entity.Name, entity.Description));

                            await _externalEventProducer.Publish(externalEvent, new CancellationToken());

                            return _mapper.Map<ProductDto>(entity);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            return null;

        }
    }
}
