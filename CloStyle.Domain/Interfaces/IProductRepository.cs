using CloStyle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task Add(Product product);
        Task<Product?> GetProductById(int id);
        Task<IEnumerable<Product>> GetByBrandId(int brandId);
        Task DeleteProduct(int id);
        Task Commit();
    }
}
