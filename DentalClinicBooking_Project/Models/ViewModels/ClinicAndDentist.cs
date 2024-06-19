namespace DCB1.Models.ViewModels
{
	public class ClinicAndDentist
	{
		public List<ClinicWithAddress> clinicWithAddress { get; set; }

		public List<DentistWithClinicName> dentistWithClinicName { get; set; }
	}
}
