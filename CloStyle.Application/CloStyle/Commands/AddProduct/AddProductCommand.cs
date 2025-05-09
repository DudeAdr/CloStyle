using CloStyle.Application.CloStyle.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Commands.AddProduct
{
    public class AddProductCommand : IRequest<Unit>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        public string Description { get; set; }

        [Required]
        public int BrandId { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int GenderId { get; set; }

        public List<SizeDto> Sizes { get; set; } = new();
        public List<CategoryDto> Categories { get; set; } = new();
        public List<GenderDto> Genders { get; set; } = new();
    }
}
