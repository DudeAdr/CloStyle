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

        public UserRepository(CloStyleDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
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

        public async Task<IEnumerable<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user != null)
            {
                return await _userManager.GetRolesAsync(user);
            }
            return null;  
        }
    }
}
