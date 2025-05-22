using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Dtos.ProductDTOs
{
    public class SizeDto
    {
        public int Id { get; set; }
        public string Size { get; set; }
        public int Stock { get; set; }
    }
}
