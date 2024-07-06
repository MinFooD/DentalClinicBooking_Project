
using DentalClinicBooking_Project.Data;
using DentalClinicBooking_Project.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinicBooking_Project.Controllers
{
    public class DentistController : Controller
    {
        public DentalClinicBookingProjectContext _context;
        public IActionResult ShowDentistInfo(Guid? id)
        {
            _context = new DentalClinicBookingProjectContext();

            var dentist = _context.Dentists.Select(a => new DentistWithClinicName
            {
                DentistId = a.DentistId,
                DentistName = a.DentistName,
                Image = a.Image,
                Experience = a.Experience,
                BasicId = a.BasicId,
                AccountId = a.AccountId,
                ClinicName = a.Basic.Clinic.ClinicName,
                Description = a.Description,
                Address = a.Basic.Address
            }).FirstOrDefault(x => x.DentistId.Equals(id));

            if (dentist != null)
            {
                return View(dentist);
            }
            return View(null);
        }

        [HttpGet]
        public IActionResult ShowAllDentist(string searchString, int page = 1)
        {
            _context = new DentalClinicBookingProjectContext();

            var dentists = _context.Dentists.Select(a => new DentistWithClinicName
            {
                DentistId = a.DentistId,
                DentistName = a.DentistName,
                Image = a.Image,
                Experience = a.Experience,
                BasicId = a.BasicId,
                AccountId = a.AccountId,
                ClinicName = a.Basic.Clinic.ClinicName,
                Address = a.Basic.Address
            }).Where(d => d.DentistName.Contains(searchString) || string.IsNullOrEmpty(searchString)).ToList();

            var searchDentists = new SearchDentist
            {
                SearchString = searchString,
                dentists = dentists,
                ResultCount = dentists.Count()//tổng số phần tử
            };

            //paging
            int NoOfDentistPerPage = 4;//bao nhiêu phần tử mỗi trang
            var NoOfPaging = Math.Ceiling((decimal)dentists.Count() / NoOfDentistPerPage);//tồng số trang
            int NoOfDentistToSkip = (page - 1) * NoOfDentistPerPage;//bỏ qua bao nhiêu
            ViewBag.Page = page;//trang bao nhiêu
            ViewBag.NoOfPaging = NoOfPaging;
            searchDentists.dentists = searchDentists.dentists.Skip(NoOfDentistToSkip).Take(NoOfDentistPerPage).ToList();
            //

            return View(searchDentists);


        }
    }
}
