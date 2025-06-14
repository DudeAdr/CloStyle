using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Domain.Entities
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
        [NotMapped]
        public decimal TotalPrice => Items?.Sum(i => i.Product.Price * i.Quantity) ?? 0;
    }
}
