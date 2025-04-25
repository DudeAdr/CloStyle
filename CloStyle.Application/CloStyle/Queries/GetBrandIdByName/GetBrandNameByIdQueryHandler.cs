using AutoMapper;
using CloStyle.Domain.Entities;
using CloStyle.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.GetBrandNameById
{
    public class GetBrandNameByIdQueryHandler : IRequestHandler<GetBrandNameByIdQuery, string>
    {
        private IBrandRepository _repository;

        public GetBrandNameByIdQueryHandler(IBrandRepository repository, IMapper mapper)
        {
            _repository = repository;
        }
        public async Task<string> Handle(GetBrandNameByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetBrandNameById(request.brandId);
        }
    }

}
