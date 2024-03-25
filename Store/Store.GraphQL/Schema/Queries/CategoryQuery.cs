using HotChocolate.Authorization;
using Store.Domain.Dtos;
using Store.Domain.Services;

namespace Store.GarphQL.Schema.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class CategoryQuery
    {
        private readonly ICategoryService _service;

        public CategoryQuery(ICategoryService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategory()
        {
            return await _service.GetAllCategory();
        }
        [Authorize(Roles = new[] { "administrator", "customer" })]
        public async Task<CategoryDto> GetCategoryById(Guid id)
        {
            return await _service.GetCategoryById(id);
        }
    }
}
