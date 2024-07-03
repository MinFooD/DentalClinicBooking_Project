using System.ComponentModel.DataAnnotations;

namespace DentalClinicBooking_Project.Models.ViewModels
{
	public class LoginVM
	{
		[Required(ErrorMessage ="Gmail can not be blank.")]
		public string Gmail { get; set; }
		[Required(ErrorMessage = "Password can not be blank.")]
		public string Password { get; set; }
	}
}
