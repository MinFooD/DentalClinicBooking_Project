using DentalClinicBooking_Project.Models.Domain;

namespace DentalClinicBooking_Project.Models.ViewModels.Dentist.DentistAppointment
{
	public class ShowDentistInfoModel
	{
		public string DentistName { get; set; }

		public string Image { get; set; }

		public string? Gender { get; set; }

		public string Phone { get; set; }

		public string Email { get; set; }
		public IEnumerable<SlotOfClinic> SlotOfClinics { get; set; }
		public static string GetGender(bool? gender)
		{
			if (gender == true)
			{
				return "Nam";
			}
			else if (gender == false)
			{
				return "Nữ";
			}
			else
			{
				return "Không xác định";
			}
		}
	}
}
