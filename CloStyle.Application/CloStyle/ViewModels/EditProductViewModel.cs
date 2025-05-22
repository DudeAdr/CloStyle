using CloStyle.Application.CloStyle.Dtos.ProductDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.ViewModels
{
    public class EditProductViewModel : EditProductDto
    {
        public int Id { get; set; }
        public List<CategoryDto> Categories { get; set; } = new();
        public List<GenderDto> Genders { get; set; } = new();
        public string BrandName { get; set; }
        public int BrandId { get; set; }
    }
}
