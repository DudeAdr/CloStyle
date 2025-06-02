using CloStyle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CurrentApplicationUser
{
    public class CurrentUser
    {
        public CurrentUser(string id, string email, IEnumerable<string> roles, List<Brand> brands)
        {
            Id = id;
            Email = email;
            Roles = roles;
            Brands = brands;
        }

        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public bool IsInRole(string role) => Roles.Contains(role);
        public List<Brand> Brands { get; set; } = new List<Brand>();

        public static readonly int maxBrandsNumber = 5;
        public bool HasSpaceForBrand()
        {
            return this.Brands.Count < maxBrandsNumber;
        }
    }
}
