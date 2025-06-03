using CloStyle.Application.CloStyle.Dtos.AdminPanelDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.ViewModels.AdminPanelVM
{
    public class ShowUserRolesVM
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string SelectedRoleId { get; set; } = string.Empty;
        public RoleDto UserRole { get; set; }
        public List<RoleDto> AllAvailableRoles { get; set; } = new List<RoleDto>();
    }
}
