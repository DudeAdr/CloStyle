using CloStyle.Application.CloStyle.Dtos.BrandDTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Dtos.AdminPanelDTOs
{
    public class UserDto
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public RoleDto Role { get; set; }
        public int BrandsCount { get; set; }
        public List<BrandDto> Brands = new List<BrandDto>();
    }
}
