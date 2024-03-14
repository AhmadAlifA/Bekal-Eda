using AutoMapper;
using Framework.Core.Events;
using Framework.Core.Events.External;
using Payment.Domain.Dtos;
using Payment.Domain.Entities;
using Payment.Domain.EventEnvelopes;
using Payment.Domain.Repositories;

namespace Payment.Domain.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> GetAllPayment();
        Task<PaymentDto> GetPaymentById(Guid id);
        Task<PaymentDto> Update(PaymentUpdateDto dto);
        Task<bool> ChangeStatusPayment(PaymentStatusDto dto);
    }
    public class PaymentService : IPaymentService
    {
        private IPaymentRepository _repository;
        private readonly IMapper _mapper;
        private readonly IExternalEventProducer _externalEventProducer;
        public PaymentService(IPaymentRepository repository, IMapper mapper, IExternalEventProducer externalEventProducer)
        {
            _repository = repository;
            _mapper = mapper;
            _externalEventProducer = externalEventProducer;
        }

        public async Task<IEnumerable<PaymentDto>> GetAllPayment()
        {
            try
            {
                var result = _mapper.Map<IEnumerable<PaymentDto>>(await _repository.GetAll());
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Inner Error: {e.InnerException}");
            }
            return null;
        }

        public async Task<PaymentDto> GetPaymentById(Guid id)
        {
            return _mapper.Map<PaymentDto>(await _repository.GetById(id));
        }

        public async Task<PaymentDto> Update(PaymentUpdateDto dto)
        {
            try
            {
                if (dto.Id != new Guid())
                {
                    var existEntity = await _repository.GetById(dto.Id);
                    if (existEntity != null && existEntity.Total < dto.Pay)
                    {
                        var entity = _mapper.Map<PaymentUpdateDto, PaymentEntity>(dto, existEntity);
                        await _repository.Update(entity);
                        var result = await _repository.SaveChangesAsync();
                        if (result > 0)
                        {
                            var externalEvent = new EventEnvelope<PaymentUpdated>(
                                PaymentUpdated.Create(entity.Id, entity.Pay));

                            await _externalEventProducer.Publish(externalEvent, new CancellationToken());

                            return _mapper.Map<PaymentDto>(entity);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            return null;
        }

        public async Task<bool> ChangeStatusPayment(PaymentStatusDto dto)
        {
            try
            {
                if (dto.Id != new Guid())
                {
                    var existEntity = await _repository.GetById(dto.Id);
                    if (existEntity != null)
                    {
                        var entity = _mapper.Map<PaymentStatusDto, PaymentEntity>(dto, existEntity);
                        await _repository.Update(entity);
                        var result = await _repository.SaveChangesAsync();
                        if (result > 0)
                        {
                            var externalEvent = new EventEnvelope<PaymentStatusChanged>(
                                PaymentStatusChanged.Create(
                                entity.Id,
                                entity.Status
                                ));
                            await _externalEventProducer.Publish(externalEvent, new CancellationToken());

                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Inner Error: {e.InnerException}");
            }
            return false;
        }
    }
}
