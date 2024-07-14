namespace DentalClinicBooking_Project.Models.ViewModels.Dentist
{
    public class UpdateDentistVM
    {
        public Guid DentistId { get; set; }

        public string DentistName { get; set; }

        public string Image { get; set; }

        public string Experience { get; set; }

        public string Description { get; set; }

        public string Gmail { get; set; }

        public string Password { get; set; }
    }
}
