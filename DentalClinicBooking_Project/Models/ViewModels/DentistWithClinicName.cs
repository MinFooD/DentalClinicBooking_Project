namespace DentalClinicBooking_Project.Models.ViewModels
{
	public class DentistWithClinicName
	{
		public Guid DentistId { get; set; }

		public Guid ClinicId { get; set; }
		public string DentistName { get; set; } = null!;

		public string Image { get; set; } = null!;

		public string Experience { get; set; } = null!;

		public Guid? BasicId { get; set; }

		public Guid? AccountId { get; set; }

		public string ClinicName { get; set; } = null!;

		public string Description { get; set; } = null!;

		public string Address { get; set; } = null!;
	}
}
