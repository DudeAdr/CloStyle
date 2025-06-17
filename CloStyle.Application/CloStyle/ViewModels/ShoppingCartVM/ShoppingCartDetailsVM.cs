using CloStyle.Application.CloStyle.Dtos.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.ViewModels.ShoppingCartVM
{
    public class ShoppingCartDetailsVM
    {
        public List<ProductForShoppingCartDto> Items { get; set; } = new();
        public decimal TotalCartPrice { get; set; }
    }
}
