using AutoMapper;
using Repository.Entities;
using Service.DTOs;
namespace Service.Utils
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<UserSignUpDto, User>();
            // תוסיפי כאן מיפויים נוספים בהמשך כמו:
            // CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}   