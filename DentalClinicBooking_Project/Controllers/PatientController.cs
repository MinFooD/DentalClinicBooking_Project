using Microsoft.AspNetCore.Mvc;

namespace DentalClinicBooking_Project.Controllers
{
	public class PatientController : Controller
	{
		public IActionResult ChangeInformation()
		{
			return View();
		}
	}
}
