using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication4.Data;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext _context)
        {
            this._context = _context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Service()
        {
            var s = _context.services.ToList();
            return View(s);
        }
        public IActionResult About_Us()
        {
            return View();
        }
        public IActionResult ProviderForm()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ProviderForm(ProviderForm model )
        {
            if (ModelState.IsValid)
            {
                _context.ProviderForms.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else 
            return BadRequest();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
