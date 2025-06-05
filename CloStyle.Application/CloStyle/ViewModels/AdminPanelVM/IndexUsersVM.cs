using CloStyle.Application.CloStyle.Dtos.AdminPanelDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.ViewModels.AdminPanelVM
{
    public class IndexUsersVM
    {
        public List<UserDto> Users { get; set; }
        public List<RoleDto> AllRoles { get; set; }
    }
}
