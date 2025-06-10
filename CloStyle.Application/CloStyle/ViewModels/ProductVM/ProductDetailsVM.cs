using CloStyle.Application.CloStyle.Dtos.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.ViewModels.ProductVM
{
    public class ProductDetailsVM : ProductDto
    {
        public string BrandName { get; set; }
        public int BrandId { get; set; }
        public int SizeId { get; set; }
    }
}
