using Microsoft.AspNetCore.Mvc;

namespace DentalClinicBooking_Project.Controllers.Admin
{
    public class AdminController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
    }
}
