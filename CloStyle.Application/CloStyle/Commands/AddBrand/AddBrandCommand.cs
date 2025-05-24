using CloStyle.Application.CloStyle.Dtos.BrandDTOs;
using MediatR;

namespace CloStyle.Application.CloStyle.Commands.AddBrand
{
    public class AddBrandCommand : BrandDto, IRequest<Unit>
    {
    }
}
