using LookUp.Domain.Dtos;
using LookUp.Domain.Services;

namespace Lookup.GraphQL.Schema.Mutation
{
    [ExtendObjectType(typeof(Mutation))]
    public class AttributeMutation
    {
        private readonly IAttributeService _service;
        public AttributeMutation(IAttributeService service)
        {
            _service = service;
        }
        public async Task<AttributeDto> AddAttributeAsync(AttributeDto dto)
        {
            return await _service.AddAttribute(dto);
        }
        public async Task<AttributeExceptStatusDto> UpdateAttributeAsync(AttributeExceptStatusDto dto)
        {
            try
            {
                var result = await _service.UpdateAttribute(dto);
                if (result)
                    return dto;

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            return null;
        }

        public async Task<AttributeStatusDto> ChangedStatusAsync(AttributeStatusDto dto)
        {
            try
            {
                var result = await _service.ChangeStatusAttribute(dto);
                if (result)
                    return dto;

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            return null;
        }
    }
}
