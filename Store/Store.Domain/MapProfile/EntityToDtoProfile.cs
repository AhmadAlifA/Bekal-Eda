using AutoMapper;
using Store.Domain.Dtos;
using Store.Domain.Entities;
using Store.Domain.EventEvelopes.Product;

namespace Store.Domain.MapProfile
{
    public class EntityToDtoProfile: Profile
    {
        public EntityToDtoProfile() : base("Entity to Dto profile")
        {
            CreateMap<ProductEntity, ProductDto>();
            CreateMap<AttributeEntity, AttributeDto>();

            CreateMap<CategoryEntity, CategoryDto>();
            CreateMap<CategoryDto, CategoryEntity>();
            CreateMap<CategoryInputDto, CategoryEntity>();
            CreateMap<CategoryStatusDto, CategoryEntity>();

            CreateMap<ProductDto, ProductEntity>();
            CreateMap<ProductCreateDto, ProductEntity>();
            CreateMap<ProductUpdateDto, ProductEntity>();
            CreateMap<ProductCategoryDto, ProductEntity>();
            CreateMap<ProductAttributeDto, ProductEntity>();
            CreateMap<ProductPriceVolumeDto, ProductEntity>();
            CreateMap<ProductStockSoldDto, ProductEntity>();
            CreateMap<ProductStatusChanged, ProductEntity>();
        }
    }
}
