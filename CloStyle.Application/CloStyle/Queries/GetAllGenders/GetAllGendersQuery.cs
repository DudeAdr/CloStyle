﻿using CloStyle.Application.CloStyle.Dtos.ProductDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.GetAllGenders
{
    public class GetAllGendersQuery : IRequest<IEnumerable<GenderDto>>
    {
    }
}
