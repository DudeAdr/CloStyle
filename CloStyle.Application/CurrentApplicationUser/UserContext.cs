﻿using CloStyle.Domain.Entities;
using CloStyle.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CurrentApplicationUser
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }

    public class UserContext : IUserContext
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IUserRepository _userRepository;

        public UserContext(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public CurrentUser? GetCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null)
            {
                throw new InvalidOperationException("User context is not present");
            }

            if (user.Identity == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            var id = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
            var role = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            var brands = _userRepository.GetUserBrandsAsync(id).GetAwaiter().GetResult();

            return new CurrentUser(id, email, role, brands);
        }
    }
}
