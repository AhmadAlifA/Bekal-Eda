using HotChocolate.Authorization;
using HotChocolate.Types;
using Payment.Domain.Dtos;
using Payment.Domain.Services;

namespace Payment.GraphQL.Schema.Mutation
{
    [ExtendObjectType(typeof(Mutation))]
    public class PaymentMutation
    {
        private readonly IPaymentService _service;
        public PaymentMutation(IPaymentService service)
        {
            _service = service;
        }

        [Authorize(Roles = new[] { "administrator", "customer" })]
        public async Task<PaymentDto> PaymentTransactionAsync(PaymentUpdateDto dto)
        {
            var result = await _service.Update(dto);
            if (result != null)
                return result;
            return null;
        }

        [Authorize(Roles = new[] { "administrator", "customer" })]
        public async Task<PaymentStatusDto?> ChangeCancelPayment(PaymentStatusDto dto)
        {
            dto.Status = Framework.Core.Enums.CartStatusEnum.Canceled;
            var result = await _service.ChangeStatusPayment(dto);
            if (result)
                return dto;
            return null;
        }
    }
}
