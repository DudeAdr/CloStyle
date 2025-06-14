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
    public class ProductRepository : IProductRepository
    {
        private CloStyleDbContext _dbContext;

        public ProductRepository(CloStyleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);

            if (product != null)
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetByBrandId(int brandId)
        {
            return await _dbContext.Products
                .Where(p => p.Brand.Id == brandId)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Gender)
                .Include(p => p.ProductSizes)
                .ThenInclude(ps => ps.Size)
                .ToListAsync();
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await _dbContext.Products
                .Include(p => p.ProductSizes)
                    .ThenInclude(s => s.Size)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
