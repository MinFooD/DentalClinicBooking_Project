using DentalClinicBooking_Project.Data;
using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels.ClinicModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DentalClinicBooking_Project.Controllers
{
    public class ClinicController : Controller
    {
        private readonly DentalClinicBookingProjectContext _context;
		private readonly IHttpContextAccessor _contx;

		public ClinicController(DentalClinicBookingProjectContext context, IHttpContextAccessor contx)
        {
            _context = context;
			_contx = contx;
		}
        [HttpGet]
        public IActionResult AddClinic()
        {
			string ownerString = _contx.HttpContext.Session.GetString("owner");
			if (!string.IsNullOrEmpty(ownerString))
			{
				var owner = JsonConvert.DeserializeObject<Owner>(ownerString);
				var services = _context.Services?.ToList();
				if (services == null)
				{
					return BadRequest("Services are not available.");
				}
				var slot = _context.Slots.ToList();

				var model = new AddClinicVM
				{
					OwnerId = owner.OwnerId,
					AvailableServices = services.Select(s => new ServiceVM
					{
						ServiceId = s.ServiceId,
						ServiceName = s.ServiceName
					}).ToList(),
					Slots = slot.Select(x => new SlotVM
					{
						SlotId = x.SlotId,
					}).ToList()
				};
				return View(model);
			}
			return View();
        }
        [HttpPost]
		public IActionResult AddClinic(AddClinicVM addClinicVM)
		{
			if(ModelState.IsValid)
			{
				var clinic = new Clinic
				{
					ClinicName = addClinicVM.ClinicName,
					MainImage = addClinicVM.MainImage,
					Description = addClinicVM.Description,
					OwnerId = addClinicVM.OwnerId,
				};
				string imageUrlsJson = addClinicVM.ImageUrlsJson;
				List<string> imageUrls = JsonConvert.DeserializeObject<List<string>>(addClinicVM.ImageUrlsJson);
				foreach (var imageUrl in imageUrls)
				{
					clinic.ClinicImages.Add(new ClinicImage
					{
						Image = imageUrl,
						ClinicId = clinic.ClinicId
					});
				}
				foreach (var serviceId in addClinicVM.ServiceIds)
				{
					var service = _context.Services.Find(serviceId);
					if (service != null)
					{
						clinic.Services.Add(service);
					}
				}
				foreach (var slotVm in addClinicVM.Slots)
				{
					var slot = _context.Slots.Find(slotVm.SlotId);
					if (slot != null)
					{
						clinic.SlotOfClinics.Add(new SlotOfClinic
						{
							SlotId = slot.SlotId,
							ClinicId = clinic.ClinicId,
							StartTime = TimeOnly.Parse(slotVm.StartTime),
							EndTime = TimeOnly.Parse(slotVm.EndTime)
						});
					}
				}
				_context.Clinics.Add(clinic);
				_context.SaveChanges();
				return View(addClinicVM);
			}
			var services = _context.Services?.ToList();
			var slots = _context.Slots?.ToList();
			addClinicVM.AvailableServices = services?.Select(s => new ServiceVM
			{
				ServiceId = s.ServiceId,
				ServiceName = s.ServiceName
			}).ToList();
			addClinicVM.Slots = slots?.Select(x => new SlotVM
			{
				SlotId = x.SlotId,
			}).ToList();
			return View(addClinicVM);
		}

		public IActionResult ShowAllClinicForOwner()
		{
            string ownerString = _contx.HttpContext.Session.GetString("owner");
			if (!string.IsNullOrEmpty(ownerString))
			{
				var owner = JsonConvert.DeserializeObject<Owner>(ownerString);
				var clinics = _context.Clinics?.Where(x => x.OwnerId.Equals(owner.OwnerId)).Include(x => x.ClinicImages).ToList();
				if (clinics != null)
				{
					return View(clinics);
				}
			}
			return RedirectToAction("HomePage","Home");
		}
	}
}
