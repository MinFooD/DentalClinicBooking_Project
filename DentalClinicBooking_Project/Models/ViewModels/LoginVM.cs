using System.ComponentModel.DataAnnotations;

namespace DentalClinicBooking_Project.Models.ViewModels
{
	public class LoginVM
	{
		[Required(ErrorMessage = "Gmail không được để trống")]
		public string UserName { get; set; }


		[Required(ErrorMessage = "Mật khẩu không được để trống")]
		public string Password { get; set; }

	} 
}
