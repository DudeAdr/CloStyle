using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle
{
    public class BrandDto
    {
        public string Name { get; set; } = default!;
        [Required(ErrorMessage = "Logo image is required.")]
        public IFormFile? ImageFile { get; set; }

        public string? ImgPath { get; set; }
    }
}
