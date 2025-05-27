using CloStyle.Application.CloStyle.Dtos.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.ViewModels.ProductVM
{
    public class ProductsByBrandViewModel
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public bool IsEditable { get; set; }
        public IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
