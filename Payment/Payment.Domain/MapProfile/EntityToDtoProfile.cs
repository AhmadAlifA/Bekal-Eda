using AutoMapper;
using Payment.Domain.Dtos;
using Payment.Domain.Entities;

namespace Payment.Domain.MapProfile
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile() : base("Entity to Dto profile")
        {
            CreateMap<CartEntity, CartDto>();
            CreateMap<CartProductEntity, CartProductDto>();

            CreateMap<CartEntity, PaymentEntity>();
            CreateMap<CartProductEntity, PaymentEntity>();

            CreateMap<PaymentEntity, PaymentDto> ();
            CreateMap<PaymentDto, PaymentEntity>();
        }
    }
}
