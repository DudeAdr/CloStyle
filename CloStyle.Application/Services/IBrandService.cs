using CloStyle.Domain.Entities;

namespace CloStyle.Application.Services
{
    public interface IBrandService
    {
        Task Add(Brand brand);
    }
}