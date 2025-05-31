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
        Task<IEnumerable<string>> GetUserRolesAsync(string userId);
        Task<IEnumerable<ApplicationUser>> GetApplicationUsersAsync();
    }
}
