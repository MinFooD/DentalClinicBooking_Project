using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels.BookingClinicModels;

namespace DentalClinicBooking_Project.Repositories
{
    public interface IClinicAppointmentScheduleRepository
    {
        Task<ClinicAppointmentSchedule?> GetAsync(Guid id);
        Task<ClinicAppointmentSchedule?> GetDuplicateAsync(
            ClinicAppointmentSchedule clinicAppointmentSchedule);
        Task<List<BookingSlot>> GetSlotAsync(
            DateOnly date,
            Guid clinicId,
            Guid basicId);
        Task<ClinicAppointmentSchedule> AddAsync(ClinicAppointmentSchedule clinicAppointmentSchedule);
    }
}
