using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Commands.AddBrand
{
    public class AddBrandCommand : BrandDto, IRequest<Unit>
    {

    }
}
