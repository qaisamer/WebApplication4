using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;
using WebApplication4.viewmodel;

namespace WebApplication4.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserMangeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public UserMangeController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            
        }
        public async Task<IActionResult> Index()
        {
            var roles = await userManager.Users.ToListAsync();
           var user = roles.Select(user => new UsersViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FristName,
                LastName = user.LastName,
                Roles = userManager.GetRolesAsync(user).Result
            }).ToList();
            
            //var user =  await userManager.Users.Select(
            //    user => new UsersViewModel()
            //    {   Id=user.Id,
            //        Email = user.Email,
            //        FirstName = user.FristName,
            //        LastName = user.LastName,
            //        Roles =  userManager.GetRolesAsync(user).Result
            //    }).ToListAsync();

            return View(user);
        }
        
        public async Task<IActionResult> ManageUserRoles(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null) { return NotFound(); }

            var role = await roleManager.Roles.ToListAsync();
            var user_role_manage = new UserRoleManageView {
                Id = user.Id,
                Username = user.UserName,
                Roles = role.Select(role => new UserRoleView {
                RoleId=role.Id,
                RoleName=role.Name,
                IsSelected = userManager.IsInRoleAsync(user,role.Name).Result
                }).ToList()
            };
            return View(user_role_manage);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageUserRoles(UserRoleManageView model)
        {
            var user = await userManager.FindByIdAsync(model.Id);
            if (user == null) { return NotFound(); }

            var userroles = await userManager.GetRolesAsync(user);
            foreach (var role in model.Roles)
            {
                if (userroles.Any(r => r == role.RoleName) && !role.IsSelected)
                    await userManager.RemoveFromRoleAsync(user, role.RoleName);
                if (!userroles.Any(r => r == role.RoleName) && role.IsSelected)
                    await userManager.AddToRoleAsync(user, role.RoleName);

            }
            return RedirectToAction("Index");


        }

    }
}
