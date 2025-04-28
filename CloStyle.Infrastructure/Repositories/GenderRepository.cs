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
    }
}
