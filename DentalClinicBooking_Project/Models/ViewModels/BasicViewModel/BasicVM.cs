using DentalClinicBooking_Project.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace DentalClinicBooking_Project.Models.ViewModels.BasicViewModel
{
	public class BasicVM
	{
		public Guid BasicId { get; set; }
		[Required()]
		public string BasicName { get; set; }
		[Required()]
		public string Address { get; set; }
		[Required()]
		public string Phone { get; set; }

		public Guid? ClinicId { get; set; }
		public List<Clinic> clicics { get; set; }
	}
}
