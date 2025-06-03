using CloStyle.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityRole> GetUserRoleAsync(string userId);
        Task<IEnumerable<ApplicationUser>> GetApplicationUsersAsync();
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<List<Brand>> GetUserBrandsAsync(string userId);
        Task<List<IdentityRole>> GetAllAvaillableRolesAsync();
        Task<IdentityRole?> GetRoleNameById(string roleId);
    }
}
