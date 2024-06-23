using AutoMapper;
using MomAndBaby.Dto.UserDto;
using MomAndBaby.Entity;

namespace MomAndBaby.Configuration.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<LoginUserDto, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
                .ReverseMap();
        }
    }
}
