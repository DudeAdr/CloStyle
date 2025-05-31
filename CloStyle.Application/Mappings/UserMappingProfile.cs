using AutoMapper;
using CloStyle.Application.CloStyle.Dtos.AdminPanelDTOs;
using CloStyle.Domain.Entities;

namespace CloStyle.Application.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Roles, opt => opt.Ignore())
                .ForMember(dest => dest.BrandsCount, opt => opt.Ignore());
        }
    }
}
