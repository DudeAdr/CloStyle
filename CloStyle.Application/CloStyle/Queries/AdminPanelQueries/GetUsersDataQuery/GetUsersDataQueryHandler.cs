using AutoMapper;
using CloStyle.Application.CloStyle.Dtos.AdminPanelDTOs;
using CloStyle.Application.CloStyle.Dtos.BrandDTOs;
using CloStyle.Application.CurrentApplicationUser;
using CloStyle.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.AdminPanelQueries.GetUsersDataQuery
{
    public class GetUsersDataQueryHandler : IRequestHandler<GetUsersDataQuery, List<UserDto>>
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
        public async Task<List<UserDto>> Handle(GetUsersDataQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();

            if (currentUser == null || !currentUser.IsInRole("Admin"))
            {
                return new List<UserDto>();
            }

            var users = await _userRepository.GetApplicationUsersAsync();
            var mappedUsers = _mapper.Map<List<UserDto>>(users);

            foreach (var user in mappedUsers)
            {
                var roles = await _userRepository.GetUserRolesAsync(user.Id);
                var brands = await _userRepository.GetUserBrandsAsync(user.Id);

                user.Roles = roles ?? new List<string>();                
                user.Brands = _mapper.Map<List<BrandDto>>(brands);
                user.BrandsCount = brands.Count;
            }

            return mappedUsers;
        }
    }
}
