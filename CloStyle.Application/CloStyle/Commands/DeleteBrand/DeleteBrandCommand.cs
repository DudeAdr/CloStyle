using CloStyle.Application.CloStyle.Dtos.BrandDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Commands.DeleteBrand
{
    public class DeleteBrandCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgPath { get; set; }
        public bool IsEditable { get; set; }
    }
}
