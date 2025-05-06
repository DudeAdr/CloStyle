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
    public class CategoryRepository : ICategoryRepository
    {
        private CloStyleDbContext _dbContext;

        public CategoryRepository(CloStyleDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            return await _dbContext.Categories.FirstAsync(c => c.Id == id);
        }

        public async Task<int> GetCategoryIdByName(string name) =>
            (await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower()))?.Id ?? -1;
    }
}
