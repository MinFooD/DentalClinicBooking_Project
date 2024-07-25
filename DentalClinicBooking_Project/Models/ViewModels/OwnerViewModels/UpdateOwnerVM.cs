using System.ComponentModel.DataAnnotations;

namespace DentalClinicBooking_Project.Models.ViewModels.OwnerViewModels
{
	public class UpdateOwnerVM
	{
		public Guid? OwnerId { get; set; }
		public string Gmail { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string OwnerName { get; set; }
		public string Experience { get; set; }
		public string Image { get; set; }
		public Guid? AccountId { get; set; }
	}
}
