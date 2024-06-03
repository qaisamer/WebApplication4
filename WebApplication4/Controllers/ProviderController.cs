using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication4.Data;
using WebApplication4.Data.Migrations;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Authorize(Roles = "Provider")]
    public class ProviderController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProviderController(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        public IActionResult Index()
        {
            var reqs = _context.customerReqs.ToList();
            return View(reqs);
        }
        [HttpGet]
        public IActionResult ProviderLocation()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ProviderLocation(ProviderLocation providerLocation, int id) {
            if (providerLocation == null || !ModelState.IsValid || id == null)
            {
                return BadRequest();
                //new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "Invalid data.");
            }
            var customerLocation = _context.customerReqs.FirstOrDefault(x=>x.Id ==id);
            // Save data to the database
            var locationModel = new ProviderLocation
            {
                Latitude = providerLocation.Latitude,
                Longitude = providerLocation.Longitude,
                SubmittedAt = DateTime.Now
        };
            _context.providerLocations.Add(locationModel);
            _context.SaveChanges();
            return Json(new { success = true,
                customerLatitude = customerLocation.Latitude,
                customerLongitude = customerLocation.Longitude,
                id = customerLocation.Id
            });
          
        }
        public ActionResult DirectionsMap(double? customerLatitude, double? customerLongitude,int id)
        {
            var providerLocation =  _context.providerLocations.OrderByDescending(m => m.SubmittedAt).FirstOrDefault();

            if ( providerLocation == null)
            {
                // Handle case where data is not found
                return RedirectToAction("NoDataFound");
            }
            ViewBag.id = id;
            ViewBag.CustomerLatitude = customerLatitude;
            ViewBag.CustomerLongitude = customerLongitude;
            ViewBag.ProviderLatitude = providerLocation.Latitude;
            ViewBag.ProviderLongitude = providerLocation.Longitude;
            return View();
        }
        public ActionResult Finish(int id)
        {


            var req = _context.customerReqs.FirstOrDefault(x => x.Id == id);

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (req == null)
            {
                return BadRequest();
            }
            else
            {
                var order = new Order {
                UserId=userId,
                Service=req.Service,
                Description=req.Description,
                SubmittedAt =DateTime.Now
                };
                _context.orders.Add(order);
                _context.SaveChanges();
                _context.customerReqs.Remove(req);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

        }
    }
}
