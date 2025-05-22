using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Partico.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public IndexModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> Products { get; set; }

        public void OnGet()
        {
            Products = _productRepository.GetAllProducts().ToList();
        }
    }
}