using CloStyle.Application.CurrentApplicationUser;
using CloStyle.Domain.Entities;
using CloStyle.Domain.Interfaces;
using CloStyle.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CloStyle.Infrastructure.Repositories
{
    public class OwnerPanelRepository : IOwnerPanelRepository
    {
        private IUserContext _userContext;
        private CloStyleDbContext _dbContext;

        private CurrentUser? currentUser => _userContext.GetCurrentUser();

        public OwnerPanelRepository(IUserContext userContext, CloStyleDbContext dbContext)
        {
            _userContext = userContext;
            _dbContext = dbContext;
        }
        private bool IsUserAnOwner()
        {
            return currentUser != null && currentUser.IsInRole("Owner");
        }

        public async Task<int> GetNumberOfProductInUsersCarts()
        {
            return await _dbContext.ShoppingCartItems
                .Where(i => i.Product.Brand.CreatedById == currentUser.Id)
                .SumAsync(i => i.Quantity);
        }

        public async Task<int> GetOwnerBrandsAmount()
        {
            if (IsUserAnOwner())
            {
                var amount = currentUser.Brands.Count;
                return await (amount > 0 ? Task.FromResult(amount) : Task.FromResult(0));
            }
            throw new UnauthorizedAccessException("User is not an owner.");
        }

        public async Task<int> GetOwnerProductsAmount()
        {
            return await _dbContext.Products
                .Where(p => p.CreatedById == currentUser.Id)
                .CountAsync();
        }

        public async Task<List<ShoppingCartItem>> GetProductsInUserCarts()
        {
            return await _dbContext.ShoppingCartItems
                .Include(i => i.Product)
                    .ThenInclude(p => p.Brand)
                .Include(i => i.Product)
                    .ThenInclude(p => p.Category)
                .Include(i => i.Product)
                    .ThenInclude(p => p.Gender)
                .Include(i => i.ProductSize)
                    .ThenInclude(ps => ps.Size)
                .Include(i => i.ShoppingCart)
                .Where(i => i.Product.Brand.CreatedById == currentUser.Id)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalIncome()
        {
            //REFACTOR LATER AFTER implementing Orders and pdf receipts
            var productsInCarts = await GetProductsInUserCarts();
            return productsInCarts.Sum(i => i.Product.Price * i.Quantity);
        }
    }
}
