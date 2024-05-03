using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Data;
using WebApplication4.Models;
using WebApplication4.viewmodel;
 
namespace WebApplication4.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ServicesController(ApplicationDbContext _context)
        {
            this._context = _context;
            
        }
        public IActionResult Index()
        {
            var s = _context.services.ToList();
            return View(s);
        }
        public IActionResult CreatService()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatService(ServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var service = new Services { Name = model.Name, Price = model.Price, Description = model.Description };
                _context.services.Add(service);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return BadRequest();
        }
        [HttpGet]
        public IActionResult DeleteService(int Id)
        {
            var service = _context.services.FirstOrDefault(x => x.Id == Id);
            if (service == null)
            {
                return NotFound();
            }

            else { ServiceViewModel s = new ServiceViewModel { Name = service.Name, Price = service.Price, Description = service.Description };
                return View(s);
            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteService(ServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var d = _context.services.FirstOrDefault(x => x.Id == model.Id);
                _context.services.Remove(d);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
        return BadRequest(model);
        }
        [HttpGet]
        public IActionResult UpdateService(int Id)
        {
            var service = _context.services.FirstOrDefault(x => x.Id == Id);
            if (service == null)
            {
                return NotFound();
            }
            else
            {
                ServiceViewModel s = new ServiceViewModel { Id =service.Id, Name = service.Name, Price = service.Price, Description = service.Description };
                return View(s);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateService(ServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var u=  _context.services.FirstOrDefault(x => x.Id == model.Id);
                u.Name = model.Name;
                u.Price = model.Price;
                u.Description = model.Description;
                _context.services.Update(u);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return BadRequest(model);
        }

    }
}
