using HotChocolate.Authorization;
using Order.Domain.Dtos;
using Order.Domain.Services;

namespace Order.GarphQL.Schema.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class CartProductQuery
    {
        private readonly ICartProductService _service;
        public CartProductQuery(ICartProductService service)
        {
            _service = service;
        }

        [Authorize(Roles = new[] { "administrator", "customer" })]
        public async Task<IEnumerable<CartProductDto>> GetAllCartProduct()
        {
            return await _service.GetAllCartProduct();
        }

        [Authorize(Roles = new[] { "administrator", "customer" })]
        public async Task<CartProductDto> GetCartProductById(Guid id)
        {
            return await _service.GetCartProductById(id);
        }
    }
}
