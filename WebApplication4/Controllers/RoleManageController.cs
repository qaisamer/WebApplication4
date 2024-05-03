using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4.viewmodel;

namespace WebApplication4.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RoleManageController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public RoleManageController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
                
        }
        public async Task<IActionResult> Index()
        {
            var role = await roleManager.Roles.ToListAsync();
            return View(role);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(RoleViewModel model)
        { 
            if (!ModelState.IsValid)
                return View("Index", await roleManager.Roles.ToListAsync());

            if (await roleManager.RoleExistsAsync(model.Name))
            {
                ModelState.AddModelError("Name", "role already registerd");
                return View("Index",await roleManager.Roles.ToListAsync());
            
            }

            await roleManager.CreateAsync(new IdentityRole(model.Name));
            return RedirectToAction("Index");      
        }
    }
}
