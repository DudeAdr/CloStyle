using AutoMapper;
using CloStyle.Application.CloStyle.Dtos.ProductDTOs;
using CloStyle.Domain.Entities;
using CloStyle.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.ProductQueries.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
    {
        private ICategoryRepository _categoryRepository;
        private IMapper _mapper;
        public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAll();
            var mappedCategories = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return mappedCategories;
        }
    }
}
