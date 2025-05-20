using CloStyle.Domain.Entities;
using CloStyle.Domain.Interfaces;
using CloStyle.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Infrastructure.Repositories
{
    public class SizeRepository : ISizeRepository
    {
        private CloStyleDbContext _dbContext;

        public SizeRepository(CloStyleDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Size>> GetAll()
        {
            return await _dbContext.Sizes.Include(s => s.ProductSizes).ToListAsync();
        }

        public async Task<Size> GetSizeById(int id)
        {
           return await _dbContext.Sizes.FirstAsync(s => s.Id == id);
        }

        public async Task RemoveProductSizes(int productId)
        {
            var existingSizes = _dbContext.ProductSizes.Where(ps => ps.ProductId == productId);
            _dbContext.ProductSizes.RemoveRange(existingSizes);
        }
    }
}
