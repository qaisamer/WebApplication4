using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using WebApplication4.Data;

namespace WebApplication4.Controllers
{
    public class HistoryController : Controller
    {
        private readonly ApplicationDbContext context;
        public HistoryController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = context.orders.Include(x => x.User).ToList();
           
            if (User.IsInRole("Admin"))
            {
                return View(order);
            }
            else
            {
                order = order.Where(x => x.UserId == userId).ToList();
                return View(order);
            }
           
            
        }
    }
}
