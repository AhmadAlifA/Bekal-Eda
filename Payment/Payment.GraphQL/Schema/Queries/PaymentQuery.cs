using HotChocolate.Authorization;
using HotChocolate.Types;
using Payment.Domain.Dtos;
using Payment.Domain.Services;
using System.Data;

namespace Payment.GraphQL.Schema.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class PaymentQuery
    {
        private readonly IPaymentService _service;
        public PaymentQuery(IPaymentService service)
        {
            _service = service;
        }

        [Authorize(Roles = new[] { "administrator", "customer" })]
        public async Task<IEnumerable<PaymentDto>> GetAllPayment()
        {
            return await _service.GetAllPayment();
        }
        
        [Authorize(Roles = new[] { "administrator", "customer" })]
        public async Task<PaymentDto> GetPaymentById(Guid id)
        {
            return await _service.GetPaymentById(id);
        }
    }
}
