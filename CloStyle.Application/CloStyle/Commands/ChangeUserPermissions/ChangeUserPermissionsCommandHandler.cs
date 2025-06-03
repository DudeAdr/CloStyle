using CloStyle.Domain.Entities;
using CloStyle.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Commands.ChangeUserPermissions
{
    public class ChangeUserPermissionsCommandHandler : IRequestHandler<ChangeUserPermissionsCommand, Unit>
    {
        private IUserRepository _userRepository;
        private UserManager<ApplicationUser> _userManager;

        public ChangeUserPermissionsCommandHandler(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }
        public async Task<Unit> Handle(ChangeUserPermissionsCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.UserId);
            var role = await _userRepository.GetUserRoleAsync(request.UserId);

            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Any())
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeResult.Succeeded)
                {
                    throw new Exception("Failed to remove old roles.");
                }
            }

            // Pobierz nazwę roli po ID
            var roleEntity = await _userRepository.GetRoleNameById(request.SelectedRoleId);
            if (roleEntity == null)
                throw new InvalidOperationException($"Role {request.SelectedRoleId} does not exist.");

            await _userManager.AddToRoleAsync(user, roleEntity.Name);

            return Unit.Value;
        }
    }
}
