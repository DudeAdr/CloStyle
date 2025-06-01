using CloStyle.Application.CloStyle.Dtos.BrandDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.ViewModels.AdminPanelVM
{
    public class ShowUserBrandsVM
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<BrandDto> Brands { get; set; } = new();
    }
}
