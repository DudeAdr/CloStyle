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
        Task<Dictionary<string,string>> GetUserRolesAsync(string userId);
        Task<IEnumerable<ApplicationUser>> GetApplicationUsersAsync();
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<List<Brand>> GetUserBrandsAsync(string userId);
    }
}
