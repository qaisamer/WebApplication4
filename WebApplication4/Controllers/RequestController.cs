using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication4.Data;
using WebApplication4.Models;


namespace WebApplication4.Controllers
{
    [Authorize(Roles = "User")]
    public class RequestController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RequestController(ApplicationDbContext context )
        {
            _context = context;
                   
        }
        public IActionResult RequestForm()
        {
            var s = _context.services.ToList();
            ViewBag.service = new SelectList(s, "Name", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult SaveLocation(CustomerReqForm userLocation)
        {
            if (userLocation == null || !ModelState.IsValid)
            {
                return BadRequest();
                    //new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "Invalid data.");
            }

            // Save data to the database
          
                var locationModel = new CustomerReqForm
                {
                    FirstName = userLocation.FirstName,
                    LastName = userLocation.LastName,
                    Service=userLocation.Service,
                    Email = userLocation.Email,
                    PaymentMethod=userLocation.PaymentMethod,
                    PhoneNumber = userLocation.PhoneNumber,
                    Latitude = userLocation.Latitude,
                    Longitude = userLocation.Longitude
                };

                _context.customerReqs.Add(locationModel);
                _context.SaveChanges();
        

            return Json(new { success = true });
        }
        




    }
}
