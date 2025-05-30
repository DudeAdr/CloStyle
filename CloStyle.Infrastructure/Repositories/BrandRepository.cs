﻿using CloStyle.Domain.Entities;
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
    public class BrandRepository : IBrandRepository
    {
        private readonly CloStyleDbContext _dbContext;

        public BrandRepository(CloStyleDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(Brand brand)
        {
            _dbContext.Add(brand);
            await _dbContext.SaveChangesAsync();
        }

        public Task Commit()
        {
            return _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var brand = await _dbContext.Brands.FindAsync(id);

            if (brand != null)
            {
                _dbContext.Brands.Remove(brand);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Brand>> GetAll()
        {
            return await _dbContext.Brands.ToListAsync();
        }

        public async Task<Brand?> GetBrandById(int id)
        {
            return await _dbContext.Brands.FirstAsync(b => b.Id == id);
        }

        public Task<Brand?> GetBrandByName(string name)
        {
            return _dbContext.Brands.FirstOrDefaultAsync(b => b.Name.ToLower() == name!.ToLower());
        }
        public async Task<string?> GetBrandNameById(int id)
        {
            return await _dbContext.Brands
                .Where(b => b.Id == id)
                .Select(b => b.Name)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetUserBrandAmount(string id)
        {
            return await _dbContext.Brands.CountAsync(b => b.CreatedById == id);
        }
    }
}
