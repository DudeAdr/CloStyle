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
    public class GenderRepository : IGenderRepository
    {
        private CloStyleDbContext _dbContext;

        public GenderRepository(CloStyleDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Gender>> GetAll()
        {
            return await _dbContext.Genders.ToListAsync();
        }

        public async Task<Gender?> GetGenderById(int id)
        {
            return await _dbContext.Genders.FirstAsync(g => g.Id == id);
        }

        public async Task<int> GetGenderIdByName(string name) =>
            (await _dbContext.Genders.FirstOrDefaultAsync(g => g.Name.ToLower() == name.ToLower()))?.Id ?? -1;


    }
}
