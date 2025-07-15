using AutoMapper;
using Repository.Entities;
using common.DTOs;
using AutoMapper;
using Repository.Entities;
using common.DTOs;

namespace Service.Utils
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<UserSignUpDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<ShoppingList, ShoppingListDto>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.ShoppingListItems));
            CreateMap<ShoppingListDto, ShoppingList>();
            CreateMap<ShoppingListItem, ShoppingListItemDto>().ReverseMap();
            CreateMap<ShoppingListItemDto, ShoppingListItem>()
          .ForMember(dest => dest.Product, opt => opt.Ignore());
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Stores, StoreDto>().ReverseMap();
            CreateMap<AllProductsStores, AllProductStoreDto>().ReverseMap();
        }
    }
}

