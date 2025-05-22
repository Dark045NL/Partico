using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!; // Fix: initialized as not-null
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>(); // Fix
        public ICollection<Part> Parts { get; set; } = new List<Part>();    // Fix
    }

}
