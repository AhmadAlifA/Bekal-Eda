using LookUp.Domain.Dtos;
using LookUp.Domain.Services;

namespace Lookup.GraphQL.Schema.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class AttributeQuery
    {
        private readonly IAttributeService _service;

        public AttributeQuery(IAttributeService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<AttributeDto>> GetAllAttribute()
        {
            return await _service.GetAllAttribute();
        }
    }
}
