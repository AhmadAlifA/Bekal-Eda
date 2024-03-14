using Order.Domain.Dtos;
using Order.Domain.Services;

namespace Order.GarphQL.Schema.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class CartQuery
    {
        private readonly ICartService _service;
        public CartQuery(ICartService service)
        {
            _service = service;
        }
        public async Task<IEnumerable<CartDto>> GetAllCart()
        {
            return await _service.GetAllCart();
        }
        public async Task<CartDto> GetCartById(Guid id)
        {
            return await _service.GetCartById(id);
        }
    }
}
