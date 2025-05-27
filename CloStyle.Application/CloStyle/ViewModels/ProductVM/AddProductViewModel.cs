using CloStyle.Application.CloStyle.Dtos.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.ViewModels.ProductVM
{
    public class AddProductViewModel : ProductFormDto
    {
        public int Id { get; set; }
        public List<CategoryDto> Categories { get; set; } = new();
        public List<GenderDto> Genders { get; set; } = new();
        public string BrandName { get; set; }
        public int BrandId { get; set; }
    }
}
