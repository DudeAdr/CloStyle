using CloStyle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Domain.Interfaces
{
    public interface IBrandRepository
    {
        Task Add(Brand brand);
        Task<Brand?> GetBrandByName(string name);
        Task<IEnumerable<Brand>> GetAll();
        Task<string?> GetBrandNameById(int id);
        Task Commit();
        Task<Brand?> GetBrandById(int id);
    }
}
