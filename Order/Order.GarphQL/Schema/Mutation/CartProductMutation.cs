using Order.Domain.Dtos;
using Order.Domain.Services;

namespace Order.GarphQL.Schema.Mutation
{
    [ExtendObjectType(typeof(Mutation))]
    public class CartProductMutation
    {
        private readonly ICartProductService _service;
        public CartProductMutation(ICartProductService service)
        {
            _service = service;
        }
        public async Task<CartProductDto> AddCartProductAsync(CartProductCreateDto dto)
        {
            return await _service.AddCartProduct(dto);
        }

        public async Task<CartProductDto> UpdateCartProductAsync(CartProductUpdateDto dto)
        {
            var result = await _service.UpdateCartProduct(dto);
            if (result != null)
                return result;
            return null;
        }
    }
}
