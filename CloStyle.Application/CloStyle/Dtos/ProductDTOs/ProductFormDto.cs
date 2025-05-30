﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Dtos.ProductDTOs
{
    public class ProductFormDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int GenderId { get; set; }
        public List<SizeDto> Sizes { get; set; } = new();
        public bool IsEditable { get; set; }
    }
}
