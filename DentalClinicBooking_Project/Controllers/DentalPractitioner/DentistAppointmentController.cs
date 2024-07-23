using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels.Dentist.DentistAppointment;
using DentalClinicBooking_Project.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DentalClinicBooking_Project.Controllers.DentalPractitioner
{
	public class DentistAppointmentController : Controller
	{
		private readonly IHttpContextAccessor httpContext;
		private readonly IBasicRepository basicRepository;

		public DentistAppointmentController(
			IHttpContextAccessor httpContext,
			IBasicRepository basicRepository)
		{
			this.httpContext = httpContext;
			this.basicRepository = basicRepository;
		}
		public async Task<IActionResult> ShowDentistInfo()
		{
			string? dentistString = httpContext.HttpContext?.Session.GetString("dentist");
			if (!string.IsNullOrEmpty(dentistString))
			{
				var dentist = JsonConvert.DeserializeObject<Dentist>(dentistString);
				var basic = await basicRepository.GetAsync(dentist?.BasicId ?? Guid.Empty);
				var model = new ShowDentistInfoModel
				{
					DentistName = dentist.DentistName,
					Image = dentist.Image,
					Gender = ShowDentistInfoModel.GetGender(dentist.Gender),
					Phone = dentist.Phone,
					Email = dentist.Email,
					SlotOfClinics = basic.Clinic.SlotOfClinics
				};
				return View(model);

			}
			return RedirectToAction("Login", "Login");
		}
	}
}
