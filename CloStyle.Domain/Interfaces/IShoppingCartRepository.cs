using CloStyle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Domain.Interfaces
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetShoppingCartByUserId(string userId);
        Task AddItemToShoppingCart(string userId, Product product, int sizeId, int quantity);
        Task RemoveItemFromShoppingCart(string userId, Product product, int sizeId);
    }
}
