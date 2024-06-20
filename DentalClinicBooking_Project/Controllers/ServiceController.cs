using Microsoft.AspNetCore.Mvc;

namespace DentalClinicBooking_Project.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
