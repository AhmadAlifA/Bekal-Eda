using HotChocolate.Authorization;
using Store.Domain.Dtos;
using Store.Domain.Services;

namespace Store.GarphQL.Schema.Mutation
{
    [ExtendObjectType(typeof(Mutation))]
    public class CategoryMutation
    {
        private readonly ICategoryService _service;
        public CategoryMutation(ICategoryService service)
        {
            _service = service;
        }

        //[ReadableBodyStream]
        //[Authorize]
        //[Authorize(Roles = new[] { "Customer" })]
        [Authorize(Roles = new[] { "administrator" })]
        public async Task<CategoryDto> AddAsync(CategoryInputDto dto)
        {
            return await _service.AddCategory(dto);
        }

        public async Task<CategoryInputDto> UpdateAsync(CategoryInputDto dto)
        {
            var result = await _service.Update(dto);
            if (result)
                return dto;
            return null;
        }

        public async Task<CategoryStatusDto?> ChangeStatusAsync(CategoryStatusDto dto)
        {
            var result = await _service.ChangeStatusCategory(dto);
            if (result)
                return dto;
            return null;
        }
    }
}
