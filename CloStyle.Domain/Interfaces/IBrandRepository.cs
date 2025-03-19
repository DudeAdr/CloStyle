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
        Task<Brand?> GetByName(string name);
    }
}
