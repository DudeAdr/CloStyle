using CloStyle.Domain.Entities;
using CloStyle.Domain.Interfaces;
using CloStyle.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Infrastructure.Repositories
{
    class UserRepository : IUserRepository
    {
        private CloStyleDbContext _dbContext;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public UserRepository(CloStyleDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task DeleteUserAsync(ApplicationUser? applicationUser)
        {
            if(applicationUser != null)
            {
                await _userManager.DeleteAsync(applicationUser);
            }
        }

        public async Task<List<IdentityRole>> GetAllAvaillableRolesAsync()
        {
            return await _roleManager.Roles.Select(r => new IdentityRole
            {
                Id = r.Id,
                Name = r.Name
            }).ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetApplicationUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<IdentityRole?> GetRoleNameById(string roleId)
        {
            return await _roleManager.FindByIdAsync(roleId);
        }

        public async Task<List<Brand>> GetUserBrandsAsync(string userId)
        {
            return await _dbContext.Brands
                .Where(b => b.CreatedById == userId)
                .ToListAsync();
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<IdentityRole> GetUserRoleAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            var roleName = roles.FirstOrDefault();
            if (string.IsNullOrEmpty(roleName))
            {
                return null;
            }
            var role = await _roleManager.FindByNameAsync(roleName);
            return role;
        }
    }
}
