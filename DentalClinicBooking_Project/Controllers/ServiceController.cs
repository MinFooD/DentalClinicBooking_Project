using DentalClinicBooking_Project.Data;
using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DentalClinicBooking_Project.Controllers
{
	public class ServiceController : Controller
	{
		public DentalClinicBookingProjectContext _context;
		public IActionResult Service(Guid? id)
		{
			_context = new DentalClinicBookingProjectContext();

			var services = _context.Services.ToList();

			var clinics = _context.Services.Include(s => s.Clinics)
											.ThenInclude(c => c.Basics)
											.FirstOrDefault()
											?.Clinics
											.ToList();

			if (id != null)
			{
				clinics = _context.Services.Where(x => x.ServiceId.Equals(id))
														.SelectMany(x => x.Clinics)
														.Include(x => x.Basics).ToList();
			}
			

			

			var serviceAndClinic = new ServiceAndClinic()
			{
				clinics = clinics,
				services = services
			};
			return View(serviceAndClinic);
		}
	}
}
