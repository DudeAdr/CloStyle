using CloStyle.Application.CloStyle.Dtos.ProductDTOs;
using CloStyle.Application.CloStyle.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Commands.EditProduct
{
    public class EditProductCommand : ProductFormDto, IRequest<Unit>
    {
        public int Id { get; set; }
        public List<CategoryDto> Categories { get; set; } = new();
        public List<GenderDto> Genders { get; set; } = new();
        public string BrandName { get; set; }
        public int BrandId { get; set; }
    }
}
