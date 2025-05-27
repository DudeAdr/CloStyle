using CloStyle.Application.CloStyle.Commands.DeleteProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.ViewModels.ProductVM
{
    public class DeleteProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public bool IsEditable { get; set; }
    }
}
