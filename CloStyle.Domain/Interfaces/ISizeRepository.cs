using CloStyle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Domain.Interfaces
{
    public interface ISizeRepository
    {
        Task<IEnumerable<Size>> GetAll();
        Task<Size> GetSizeById(int id);
    }
}
