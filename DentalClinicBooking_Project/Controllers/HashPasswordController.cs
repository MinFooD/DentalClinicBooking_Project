//using BCrypt.Net;
//using DentalClinicBooking_Project.Data;
//using Microsoft.AspNetCore.Mvc;

//namespace DentalClinicBooking_Project.Controllers
//{
//    public class HashPasswordController : Controller
//    {
//        DentalClinicBookingProjectContext _context;

//        public HashPasswordController(DentalClinicBookingProjectContext context)
//        {
//            _context = context;
//        }

//        public IActionResult HashPassword()
//        {
//            var accounts = _context.Accounts.ToList();
//            foreach (var account in accounts)
//            {
//                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(account.Password);
//                account.Password = hashedPassword;

//                //bool isValidVerify = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
//            }
//            _context.SaveChanges();
//            return View();
//        }
//    }
//}
