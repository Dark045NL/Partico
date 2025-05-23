using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

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