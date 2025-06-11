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
            CreateMap<ShoppingListDto, ShoppingList>().ReverseMap();
            CreateMap<ShoppingListItem, ShoppingListItemDto>().ReverseMap();
            CreateMap<ShoppingListItemDto, ShoppingListItem>()
          .ForMember(dest => dest.Product, opt => opt.Ignore());

            // תוסיפי כאן מיפויים נוספים בהמשך כמו:
            // CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}

