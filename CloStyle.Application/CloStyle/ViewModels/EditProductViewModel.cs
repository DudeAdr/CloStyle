using CloStyle.Application.CloStyle.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.ViewModels
{
    public class EditProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public List<CategoryDto> Categories { get; set; } = new();

        public int GenderId { get; set; }
        public List<GenderDto> Genders { get; set; } = new();

        public List<SizeDto> Sizes { get; set; } = new();

        public string BrandName { get; set; }
        public int BrandId { get; set; }
    }
}
