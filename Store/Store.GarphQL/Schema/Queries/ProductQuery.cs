using Store.Domain.Dtos;
using Store.Domain.Services;

namespace Store.GarphQL.Schema.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class ProductQuery
    {
        private readonly IProductService _service;
        public ProductQuery(IProductService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProduct()
        {
            return await _service.GetAllProduct();
        }
    }
}
