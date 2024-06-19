namespace DentalClinicBooking_Project.Models.ViewModels
{
	public class ClinicWithAddress
	{
		public Guid ClinicId { get; set; }

		public string ClinicName { get; set; } = null!;

		public string MainImage { get; set; } = null!;

		public string Address { get; set; } = null!;
	}
}
