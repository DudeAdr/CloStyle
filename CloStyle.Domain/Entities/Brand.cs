using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Domain.Entities
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? ImgPath { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();

        public string? CreatedById { get; set; }
        public IdentityUser? CreatedBy { get; set; }
    }
}
