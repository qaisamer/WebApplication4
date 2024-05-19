using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication4.Controllers
{
    [Authorize(Roles = "User")]
    public class RequestController : Controller
    {
        public RequestController()
        {
                   
        }
        public IActionResult RequestForm()
        {
            return View();
        }
    }
}
