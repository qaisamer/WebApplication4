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
        public IActionResult Approved() {
            return RedirectToAction("Index", "UserMange");
        }
        public IActionResult Disapproved( int id) {
            if (id == null)
            {
                return RedirectToAction("ProvidersFormApp");
            }
            else {
                var f = _context.ProviderForms.FirstOrDefault(x => x.Id == id);
                _context.ProviderForms.Remove(f);
                _context.SaveChanges();
                return RedirectToAction("ProvidersFormApp");
                }
        }
    }
}
