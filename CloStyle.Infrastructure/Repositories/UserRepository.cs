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

        public async Task<IEnumerable<ApplicationUser>> GetApplicationUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
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

        public async Task<Dictionary<string,string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roleNames = await _userManager.GetRolesAsync(user);
            var rolesDict = new Dictionary<string, string>();
            foreach(var roleName in roleNames)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    rolesDict[role.Id] = role.Name;
                }
            }
            return rolesDict;
        }
    }
}
