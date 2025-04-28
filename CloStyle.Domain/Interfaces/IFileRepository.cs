using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Domain.Interfaces
{
    public interface IFileRepository
    {
        Task<string> SaveBrandImageAsync(IFormFile file);
        Task DeleteFileAsync(string filePath);
    }
}
