using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderModel = DataAccessLayer.Models.Order;

namespace Partico.Pages.Orders

{
    public class HistoryModel : PageModel
    {
        private readonly IOrderRepository _orderRepo;

        public HistoryModel(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public List<OrderModel> Orders { get; set; } = new();


        public void OnGet()
        {
            Orders = _orderRepo.GetAllOrders().ToList();
        }

    }
}

