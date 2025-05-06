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
        Task<IEnumerable<Product>> GetAll();
        Task<IEnumerable<Product>> GetByBrandId(int brandId);
        Task<IEnumerable<Product>> GetByCategoryId(int categoryId);
        Task<IEnumerable<Product>> GetByGenderId(int genderId);
        Task<IEnumerable<Product>> GetBySizeId(int sizeId);
        Task DeleteProduct(int id);
    }
}
