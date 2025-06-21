using CloStyle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Domain.Interfaces
{
    public interface IOwnerPanelRepository
    {
        Task<int> GetOwnerProductsAmount();
        Task<int> GetOwnerBrandsAmount();
        Task<decimal> GetTotalIncome();
        Task<int> GetNumberOfProductInUsersCarts();
        Task<List<ShoppingCartItem>> GetProductsInUserCarts();
    }
}
