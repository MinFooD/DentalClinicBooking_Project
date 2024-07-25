using System.ComponentModel.DataAnnotations;

namespace DentalClinicBooking_Project.Models.ViewModels.AdminViewModels
{
    public class UpdateAccountVM
    {
		[Required]
		public Guid AccountId { get; set; }
        [Required]
        public string Gmail { get; set; }
		[Required]
		public string UserName { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		public int RoleId { get; set; }
    }
}
