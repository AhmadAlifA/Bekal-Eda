using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;
using Store.Domain.Dtos;
using Store.Domain.Services;

namespace Store.GarphQL.Schema.Mutation
{
    [ExtendObjectType(typeof(Mutation))]
    public class ProductMutation
    {
        private readonly IProductService _service;
        private IValidator<ProductCreateDto> _validator;
        public ProductMutation(IProductService service, IValidator<ProductCreateDto> validator)
        {
            _service = service;
            _validator = validator;
        }

        public async Task<ProductDto> AddProductAsync(ProductCreateDto dto)
        {
            ValidationResult result = await _validator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                throw new GraphQLException(JsonConvert.SerializeObject(result.Errors));
            }

            return await _service.AddProduct(dto);
        }

        public async Task<ProductDto> UpdateProductAsync(ProductUpdateDto dto)
        {
            var result = await _service.Update(dto);
            if (result != null)
                return result;
            return null;
        }

        public async Task<ProductDto> ChangeCategoryProductAsync(ProductCategoryDto dto)
        {
            var result = await _service.ChangeCategory(dto);
            if (result != null)
                return result;
            return null;
        }

        public async Task<ProductDto> ChangeAttributeProductAsync(ProductAttributeDto dto)
        {
            var result = await _service.ChangeAttribute(dto);
            if (result != null)
                return result;
            return null;
        }

        public async Task<ProductStatusDto> ChangeStatusProductAsync(ProductStatusDto dto)
        {
            var result = await _service.ProductChangeStatus(dto);
            if (result != null)
                return dto;
            return null;
        }
    }
}
