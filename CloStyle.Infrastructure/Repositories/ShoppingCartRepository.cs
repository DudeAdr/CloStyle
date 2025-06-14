using CloStyle.Domain.Entities;
using CloStyle.Domain.Interfaces;
using CloStyle.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace CloStyle.Infrastructure.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private CloStyleDbContext _dbContext;

        public ShoppingCartRepository(CloStyleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //LATER THINK ABOUT REFACTOR(DDD?) BCS OF TOO MUCH BUSINESS LOGIC HERE
        public async Task AddItemToShoppingCart(string userId, Product product, int sizeId, int quantity)
        {
            var availlable = await IsProductAvaillable(product, sizeId, quantity);
            if (!availlable)
            {
                throw new InvalidOperationException("Not availlable");
            }
            var cart = await GetShoppingCartByUserId(userId);

            if(cart == null)
            {
                cart = new ShoppingCart()
                {
                    UserId = userId,
                    Items = new List<ShoppingCartItem>()
                };
                _dbContext.ShoppingCarts.Add(cart);
                await _dbContext.SaveChangesAsync();
            }

            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == product.Id && i.ProductSizeId == sizeId);
            var productSizes = product.ProductSizes.FirstOrDefault(i => i.SizeId == sizeId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                productSizes.Stock -= quantity;
            }
            else
            {
                var newItem = (new ShoppingCartItem()
                {
                    ProductSizeId = sizeId,
                    Quantity = quantity,
                    ShoppingCartId = cart.Id,
                    ProductId = product.Id
                });

                cart.Items.Add(newItem);

                productSizes.Stock -= quantity;
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ShoppingCart?> GetShoppingCartByUserId(string userId)
        {
            return await _dbContext.ShoppingCarts
                .Include(c => c.Items)
                    .ThenInclude(ps => ps.Product)
                        .ThenInclude(g => g.Gender)
                .Include(c => c.Items)
                    .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.Category)

                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        private async Task<bool> IsProductAvaillable(Product product, int sizeId, int quantity)
        {
            if (product == null)
            {
                return false;
            }
            else if (product.ProductSizes.Any(c => c.SizeId == sizeId && c.Stock >= quantity))
            {
                return true;
            } 
            else
            {
                return false;
            }
        }
    }
}
