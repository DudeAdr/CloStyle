using AutoMapper;
using CloStyle.Application.CloStyle.Dtos.AdminPanelDTOs;
using CloStyle.Application.CloStyle.ViewModels.AdminPanelVM;
using CloStyle.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.AdminPanelQueries.GetUserRoleQuery
{
    public class GetUserRoleQueryHandler : IRequestHandler<GetUserRoleQuery, ShowUserRolesVM>
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public GetUserRoleQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ShowUserRolesVM> Handle(GetUserRoleQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.Id);
            var role = await _userRepository.GetUserRoleAsync(request.Id);
            var allRoles = await _userRepository.GetAllAvaillableRolesAsync();
            var mappedRoles = _mapper.Map<List<RoleDto>>(allRoles);

            RoleDto? userRoleDto = null;
            if (role != null)
            {
                userRoleDto = new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name
                };
            }

            var vm = new ShowUserRolesVM
            {
                UserId = request.Id,
                Name = (await _userRepository.GetUserByIdAsync(request.Id))?.UserName,
                UserRole = userRoleDto,
                AllAvailableRoles = mappedRoles
            };

            return vm;
        }
    }
}
