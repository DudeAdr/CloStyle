using CloStyle.Application.CloStyle;
using CloStyle.Domain.Entities;

namespace CloStyle.Application.Services
{
    public interface IBrandService
    {
        Task Add(BrandDto brand);
    }
}