using CloStyle.Application.CloStyle.Dtos.AdminPanelDTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Commands.ChangeUserPermissions
{
    public class ChangeUserPermissionsCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public string SelectedRoleId { get; set; }
    }
}
