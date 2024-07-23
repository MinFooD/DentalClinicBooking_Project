using DentalClinicBooking_Project.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinicBooking_Project.Controllers.Admin
{
    public class AdminController : Controller
    {
        private readonly IClinicRepository clinicRepository;

        public AdminController(IClinicRepository clinicRepository)
        {
            this.clinicRepository = clinicRepository;
        }

        [HttpGet]
        public  async Task<IActionResult> Home()
        {
            var list = await clinicRepository.GetAllAsync();

            return View(list);
        }

        [HttpPost]
        public IActionResult Home(Guid id)
        {
            //var clinic = await clinicRepository.GetAsync(id);
            var model = clinicRepository.UpdateStatus(id);
            
            return RedirectToAction("Home", "Admin");
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            //var clinic = await clinicRepository.GetAsync(id);
            var model = clinicRepository.Delete(id);

            return RedirectToAction("Home", "Admin");
        }

    }
}
