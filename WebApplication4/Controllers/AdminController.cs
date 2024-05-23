using Microsoft.AspNetCore.Mvc;
using WebApplication4.Data;

namespace WebApplication4.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext _context)
        {
            this._context = _context;
                
        }
        public IActionResult ProvidersFormApp()
        {
            var f = _context.ProviderForms.ToList();
            return View(f);
        }
        [HttpGet]
        public IActionResult Disapproved( int id) {
            var f = _context.ProviderForms.FirstOrDefault(x => x.Id == id);
            if (f == null)
            {
                return NotFound();
            
            }
           _context.ProviderForms.Remove(f);
            _context.SaveChanges();
            return RedirectToAction(nameof(ProvidersFormApp));
        }
    }
}
