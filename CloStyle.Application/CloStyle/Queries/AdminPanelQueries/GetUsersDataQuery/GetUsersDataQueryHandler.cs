using AutoMapper;
using CloStyle.Application.CloStyle.Dtos.AdminPanelDTOs;
using CloStyle.Application.CloStyle.Dtos.BrandDTOs;
using CloStyle.Application.CloStyle.ViewModels.AdminPanelVM;
using CloStyle.Application.CurrentApplicationUser;
using CloStyle.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.AdminPanelQueries.GetUsersDataQuery
{
    public class GetUsersDataQueryHandler : IRequestHandler<GetUsersDataQuery, IndexUsersVM>
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private IBrandRepository _brandRepository;
        private IUserContext _userContext;

        public GetUsersDataQueryHandler(IUserRepository userRepository, IMapper mapper, IBrandRepository brandRepository, IUserContext userContext)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _brandRepository = brandRepository;
            _userContext = userContext;
        }
        public async Task<IndexUsersVM> Handle(GetUsersDataQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();

            if (currentUser == null || !currentUser.IsInRole("Admin"))
            {
                return new IndexUsersVM();
            }

            var users = await _userRepository.GetApplicationUsersAsync();
            var mappedUsers = _mapper.Map<List<UserDto>>(users);
            var allRoles = await _userRepository.GetAllAvaillableRolesAsync();
            var mappedRoles = _mapper.Map<List<RoleDto>>(allRoles);

            foreach (var user in mappedUsers)
            {
                var role = await _userRepository.GetUserRoleAsync(user.UserId);
                var brands = await _userRepository.GetUserBrandsAsync(user.UserId);

                if (role != null)
                {
                    user.Role = new RoleDto
                    {
                        Id = role.Id,
                        Name = role.Name
                    };
                }

                user.Brands = _mapper.Map<List<BrandDto>>(brands);
                user.BrandsCount = brands.Count;
            }

            var vm = new IndexUsersVM
            {
                Users = mappedUsers,
                AllRoles = mappedRoles
            };

            return vm;
        }
    }
}
