using CloStyle.Application.CloStyle.Dtos.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.ViewModels.OwnerPanelVM
{
    public class OwnerPanelDataVM
    {
        public int OwnerProductsAmount { get; set; }
        public int OwnerBrandAmount { get; set; }
        public decimal TotalIncome { get; set; }
        public int NumberOfProductInUsersCarts { get; set; }
        public List<ProductForShoppingCartDto> ProductsInUserCarts { get; set; } = new();
    }
}
