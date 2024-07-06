using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels.BookingClinicModels;

namespace DentalClinicBooking_Project.Repositories
{
    public interface IClinicAppointmentScheduleRepository
    {
        Task<ClinicAppointmentSchedule?> GetAsync(Guid id);
        Task<List<BookingSlot>> GetBookingsByDateAndClinicAsync(
            DateOnly date,
            string clinicName,
            string basicName);
        Task<ClinicAppointmentSchedule> AddAsync(ClinicAppointmentSchedule clinicAppointmentSchedule);
    }
}
