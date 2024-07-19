using DentalClinicBooking_Project.Models.Domain;

namespace DentalClinicBooking_Project.Models.ViewModels.Dentist.Confirmation
{
	public class DisplayBookingInformation
	{
        public Guid Id { get; set; }
        public string? ClinicName { get; set; }
        public string? MainImage { get; set; }
        public string? BasicAddress { get; set; }
        public string? Code { get; set; }
        public DateOnly Date { get; set; }
        public SlotOfClinic? SlotOfClinics { get; set; }
        public string? BasicName { get; set; }
        public string? Service { get; set; }
        public string? PatientName { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? Gender { get; set; }
        public string? PatientAddress { get; set; }
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
        public string? Type { get; set; }

    }
}
