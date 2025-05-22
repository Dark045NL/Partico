using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Partico.Pages.Order
{
    public class CartModel : PageModel
    {
        private readonly IProductRepository _productRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly ICustomerRepository _customerRepo;

        public CartModel(IProductRepository productRepo, IOrderRepository orderRepo, ICustomerRepository customerRepo)
        {
            _productRepo = productRepo;
            _orderRepo = orderRepo;
            _customerRepo = customerRepo;
        }

        [BindProperty]
        public string? CartJson { get; set; }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(CartJson))
                return BadRequest();

            var cartItems = JsonSerializer.Deserialize<List<CartItem>>(CartJson);

            if (cartItems == null || !cartItems.Any())
                return BadRequest();

            // Dummy-klant (later vervangen door echte login)
            var customer = _customerRepo.GetAllCustomers().FirstOrDefault();
            if (customer == null)
                return BadRequest("Geen klant gevonden.");

            var productIds = cartItems.Select(i => i.Id).ToList();
            var products = _productRepo.GetAllProducts().Where(p => productIds.Contains(p.Id)).ToList();

            var order = new DataAccessLayer.Models.Order
            {
                CustomerId = customer.Id,
                OrderDate = DateTime.Now,
            };

            foreach (var product in products)
                order.Products.Add(product);

            _orderRepo.AddOrder(order);

            return RedirectToPage("/Order/Success");
        }

        public class CartItem
        {
            public int Id { get; set; }
            public int Quantity { get; set; }
        }
    }
}
