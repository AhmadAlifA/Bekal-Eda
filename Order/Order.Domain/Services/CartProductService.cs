using AutoMapper;
using Framework.Core.Events.External;
using Order.Domain.Dtos;
using Order.Domain.Entities;
using Order.Domain.Repositories;

namespace Order.Domain.Services
{
    public interface ICartProductService
    {
        Task<IEnumerable<CartProductDto>> GetAllCartProduct();
        Task<CartProductDto> GetCartProductById(Guid id);
        Task<CartProductDto> AddCartProduct(CartProductCreateDto dto);
        Task<CartProductDto> UpdateCartProduct(CartProductUpdateDto dto);

        //Task<bool> Update(CartProductUpdateDto dto);
    }
    public class CartProductService : ICartProductService
    {
        private ICartProductRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IExternalEventProducer _externalEventProducer;
        public CartProductService(ICartProductRepository repository, IMapper mapper, IExternalEventProducer externalEventProducer, IProductRepository productRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _externalEventProducer = externalEventProducer;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<CartProductDto>> GetAllCartProduct()
        {
            try
            {
                var result = _mapper.Map<IEnumerable<CartProductDto>>(await _repository.GetAll());
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Inner Error: {e.InnerException}");
            }
            return null;
        }

        public async Task<CartProductDto> GetCartProductById(Guid id)
        {
            return _mapper.Map<CartProductDto>(await _repository.GetById(id));
        }

        public async Task<CartProductDto> AddCartProduct(CartProductCreateDto dto)
        {
            try
            {
                if (dto != null)
                {
                    var getProductById = await _repository.GetProductById(dto.ProductId);
                    dto.Sku = getProductById.Sku;
                    dto.Name = getProductById.Name;
                    dto.Price = getProductById.Price;

                    var existCp = await _repository.GetByCartId(dto.CartId, dto.ProductId);


                    if (existCp != null)
                    {
                        if (getProductById.Stock >= dto.Quantity + existCp.Quantity)
                            existCp.Quantity = dto.Quantity + existCp.Quantity;
                        else
                            existCp.Quantity = getProductById.Stock;

                        var entity = await _repository.Update(existCp);
                        var result = await _repository.SaveChangesAsync();

                        if (result > 0)
                            return _mapper.Map<CartProductDto>(entity);

                    }
                    else
                    {
                        var entity = await _repository.Add(_mapper.Map<CartProductEntity>(dto));
                        var result = await _repository.SaveChangesAsync();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Inner Error: {e.InnerException}");
            }
            return new CartProductDto();
        }
        public async Task<CartProductDto> UpdateCartProduct(CartProductUpdateDto dto)
        {
            try
            {
                if (dto.Id != new Guid())
                {
                    var existEntity = await _repository.GetById(dto.Id);
                    var product = await _repository.GetProductById(existEntity.ProductId);

                    if (existEntity != null)
                    {
                        if (product.Stock < dto.Quantity)
                            return null;
                        else
                        {
                            product.Stock = product.Stock - dto.Quantity;

                            await _productRepository.Update(product);
                        }

                        var entity = _mapper.Map<CartProductUpdateDto, CartProductEntity>(dto, existEntity);
                        await _repository.Update(entity);
                        var result = await _repository.SaveChangesAsync();
    
                        return _mapper.Map<CartProductDto>(entity);
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
