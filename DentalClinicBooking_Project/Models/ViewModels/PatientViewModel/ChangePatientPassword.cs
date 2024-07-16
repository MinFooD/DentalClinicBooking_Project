namespace DentalClinicBooking_Project.Models.ViewModels.PatientViewModel
{
	public class ChangePatientPassword
	{
		public Guid? PatientId { get; set; }

		public string? PatientName { get; set; }

		public string? Phone { get; set; }

		public DateOnly? BirthDay { get; set; }

		public Guid AccountId { get; set; }

		public string OldPassword { get; set; }

		public string NewPassword { get; set; }
	}
}
