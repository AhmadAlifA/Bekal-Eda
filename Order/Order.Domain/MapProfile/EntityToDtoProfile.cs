using AutoMapper;
using Order.Domain.Dtos;
using Order.Domain.Entities;

namespace Order.Domain.MapProfile
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile() : base("Entity to Dto profile")
        {
            CreateMap<CartEntity, CartDto>();

            CreateMap<CartDto, CartEntity>();
            CreateMap<CartCreatedUpdatedDto, CartEntity>();
            CreateMap<CartStatusDto, CartEntity>();

            CreateMap<CartProductEntity, CartProductDto>();
            CreateMap<CartProductCreateDto, CartProductEntity>();
            CreateMap<CartProductUpdateDto, CartProductEntity>();
            CreateMap<CartProductCartDto, CartProductEntity>();
            CreateMap<CartProductProductDto, CartProductEntity>();

            CreateMap<UserEntity, UserDto>();
            CreateMap<ProductEntity, ProductDto>();
            CreateMap<ProductEntity, CartProductEntity>();
            //CreateMap<ProductEntity, ProductDto>();
        }
    }
}
