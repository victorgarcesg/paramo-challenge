using AutoMapper;
using Sat.Recruitment.Application.User.Add;
using Sat.Recruitment.Domain.Dtos;

namespace Sat.Recruitment.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AddUserRequest, Domain.Entities.User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.ToLower()))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone.Replace(" ", "")));

            CreateMap<Domain.Entities.User, UserDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.ToLower()))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone.Replace(" ", "")));
        }
    }
}
