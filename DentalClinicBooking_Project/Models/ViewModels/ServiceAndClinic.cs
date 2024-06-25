using DentalClinicBooking_Project.Models.Domain;

namespace DentalClinicBooking_Project.Models.ViewModels
{
	public class ServiceAndClinic
	{
		public List<Service> services { get; set; }
		public List<Clinic> clinics { get; set; }
	}
}