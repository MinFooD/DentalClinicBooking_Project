using System.ComponentModel.DataAnnotations;

namespace DentalClinicBooking_Project.Models.ViewModels.OwnerViewModels
{
	public class AddOwnerVM
	{
		[Required(ErrorMessage = "Gmail can not be blank.")]
		public string Gmail { get; set; }
		[Required(ErrorMessage = "UserName can not be blank.")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "Password can not be blank.")]
		public string Password { get; set; }
		[Required(ErrorMessage = "OwnerName can not be blank.")]
		public string OwnerName { get; set; }
		[Required(ErrorMessage = "Experience can not be blank.")]
		public string Experience { get; set; }
		[Required(ErrorMessage = "Image can not be blank.")]
		public string Image { get; set; }

		public Guid? AccountId { get; set; }
	}
}
