using AutoMapper;
using CloStyle.Application.CloStyle.Dtos.AdminPanelDTOs;
using CloStyle.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CloStyle.Application.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.BrandsCount, opt => opt.Ignore());

            CreateMap<IdentityRole, RoleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
