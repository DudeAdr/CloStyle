using AutoMapper;
using CloStyle.Application.CloStyle;
using CloStyle.Domain.Entities;
using CloStyle.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }
        public async Task Add(BrandDto brandDto)
        {
            var brand = _mapper.Map<Brand>(brandDto);
            await _brandRepository.Add(brand);
        }
    }
}
