
//using DentalClinicBooking_Project.Data;
//using DentalClinicBooking_Project.Models.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace DentalClinicBooking_Project.Controllers
//{
//	public class ContactController : Controller
//	{
//        public DentalClinicBookingProjectContext _context;
//		public IActionResult ChooseClinicContact(string searchString, int page = 1)
//		{
//			_context = new DentalClinicBookingProjectContext();

//			var clinics = _context.Clinics.Select(a => new ClinicWithAddress
//			{
//				ClinicId = a.ClinicId,
//				ClinicName = a.ClinicName,
//				MainImage = a.MainImage,
//				Address = a.Basics.FirstOrDefault().Address
//			}).Where(d => d.ClinicName.Contains(searchString) || string.IsNullOrEmpty(searchString)).ToList();

//			int NoOfClinicPerPage = 5;
//			var NoOfPaging = Math.Ceiling((decimal)clinics.Count() / NoOfClinicPerPage);
//			int NoOfDentistToSkip = (page - 1) * NoOfClinicPerPage;
//			ViewBag.Page = page;
//			ViewBag.NoOfPaging = NoOfPaging;
//			clinics = clinics.Skip(NoOfDentistToSkip).Take(NoOfClinicPerPage).ToList();

//			return View(clinics);
//		}

//		public IActionResult ShowBasicContact(Guid id)
//		{
//			_context = new DentalClinicBookingProjectContext();

//			var basics = _context.Basics.Where(x => x.ClinicId.Equals(id)).ToList(); ;
//			return View(basics);
//		}
//	}
//}
