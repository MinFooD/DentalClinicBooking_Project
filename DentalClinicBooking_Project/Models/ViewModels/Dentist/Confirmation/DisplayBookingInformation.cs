namespace DentalClinicBooking_Project.Models.ViewModels.Dentist.Confirmation
{
	public class DisplayBookingInformation
	{
		public string PatientName { get; set; }

		public string Phone { get; set; }

		public DateOnly BirthDay { get; set; }

		public bool Gender { get; set; }

		public string Address { get; set; }

	}
}
