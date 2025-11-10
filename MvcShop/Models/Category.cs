using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcShop.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public List<Product> Products { get; set; } = new();
    }
}