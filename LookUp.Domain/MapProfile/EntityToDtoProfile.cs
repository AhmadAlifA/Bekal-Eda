using AutoMapper;
using LookUp.Domain.Dtos;
using LookUp.Domain.Entities;

namespace LookUp.Domain.MapProfile
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile() : base("Entity to Dto profile")
        {
            CreateMap<AttributeEntity, AttributeDto>();
            CreateMap<AttributeDto, AttributeEntity>();

            CreateMap<AttributeExceptStatusDto, AttributeEntity>();
            CreateMap<AttributeStatusDto, AttributeEntity>();
        }
    }
}
