using AutoMapper;
using Framework.Core.Enums;
using Framework.Core.Events;
using Framework.Core.Events.External;
using Order.Domain.Dtos;
using Order.Domain.Entities;
using Order.Domain.EventEnvelopes.Cart;
using Order.Domain.Repositories;

namespace Order.Domain.Services
{
    public interface ICartService
    {
        Task<IEnumerable<CartDto>> GetAllCart();
        Task<CartDto> GetCartById(Guid id);
        Task<CartDto> AddCart(CartCreatedUpdatedDto dto);
        Task<bool> ChangeStatusCart(Guid id, CartStatusEnum status);
    }
    public class CartService : ICartService
    {
        private ICartRepository _repository;
        private ICartProductRepository _cartProductRepository;
        private readonly IMapper _mapper;
        private readonly IExternalEventProducer _externalEventProducer;

        public CartService(ICartRepository repository, IMapper mapper, IExternalEventProducer externalEventProducer, ICartProductRepository cartProductRepository)
        {
            _repository = repository;
            _cartProductRepository = cartProductRepository;
            _mapper = mapper;
            _externalEventProducer = externalEventProducer;
        }
        public async Task<CartDto> AddCart(CartCreatedUpdatedDto dto)
        {
            try
            {
                if (dto != null)
                {
                    var entity = await _repository.Add(_mapper.Map<CartEntity>(dto));
                    var result = await _repository.SaveChangesAsync();

                    if (result > 0)
                    {
                        var externalEvent = new EventEnvelope<CartCreated>(
                            CartCreated.Create(
                            entity.Id,
                            entity.Status));
                        await _externalEventProducer.Publish(externalEvent, new CancellationToken());
                        return _mapper.Map<CartDto>(entity);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Inner Error: {e.InnerException}");
            }
            return new CartDto();
        }

        public async Task<bool> ChangeStatusCart(Guid id, CartStatusEnum status)
        {
            try
            {
                bool res = true;
                var entity = await _repository.GetById(id);

                if (entity != null)
                {
                    entity.Status = status;
                    var result = await _repository.SaveChangesAsync();

                    if (result > 0)
                    {
                        List<CartProductItem> listCP = new List<CartProductItem>();

                        if (status == CartStatusEnum.Confirmed)
                        {
                            var cartProducts = await _cartProductRepository.GetCartById(entity.Id);

                            foreach (var item in cartProducts)
                            {
                                listCP.Add(new CartProductItem()
                                {
                                    Id = item.Id,
                                    ProductId = item.ProductId,
                                    Quantity = item.Quantity,
                                    Price = item.Price,
                                    Total = item.Price * (decimal)item.Quantity
                                });
                            }
                        }

                        var externalEvent = new EventEnvelope<CartStatusChanged>(
                            CartStatusChanged.Create(
                            entity.Id,
                            listCP,
                            entity.Status
                            ));
                        await _externalEventProducer.Publish(externalEvent, new CancellationToken());

                    }
                }
                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Inner Error: {e.InnerException}");
            }
            return false;
        }

        public async Task<IEnumerable<CartDto>> GetAllCart()
        {
            try
            {
                var result = _mapper.Map<IEnumerable<CartDto>>(await _repository.GetAll());
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Inner Error: {e.InnerException}");
            }
            return null;
        }

        public async Task<CartDto> GetCartById(Guid id)
        {
            return _mapper.Map<CartDto>(await _repository.GetById(id));
        }
    }
}
