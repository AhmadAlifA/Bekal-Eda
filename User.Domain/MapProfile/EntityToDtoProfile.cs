using AutoMapper;
using User.Domain.Dtos;
using User.Domain.Entities;

namespace User.Domain.MapProfile
{
    public class EntityToDtoProfile: Profile
    {
        public EntityToDtoProfile(): base("Entity to Dto profile") 
        {
            CreateMap<UserEntity, UserDto>()
                .ForMember(trg => trg.Password, org => org.Ignore());
                //.ForMember(trg => trg.Fname, org => org.MapForm(org => org.FirstName));
                
            CreateMap<UserDto, UserEntity>();

            CreateMap<UserEntity, LoginDto>();
        }
    }
}
