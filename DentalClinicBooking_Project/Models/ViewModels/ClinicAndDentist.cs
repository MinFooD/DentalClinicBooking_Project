

namespace DentalClinicBooking_Project.Models.ViewModels
{
	public class ClinicAndDentist
	{
		public List<ClinicWithAddress> clinicWithAddress { get; set; }

		public List<DentistWithClinicName> dentistWithClinicName { get; set; }
	}
}
