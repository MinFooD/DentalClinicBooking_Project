using DentalClinicBooking_Project.Data;
using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels.BookingClinicModels;
using Microsoft.EntityFrameworkCore;

namespace DentalClinicBooking_Project.Repositories
{
    public class ClinicAppointmentScheduleRepository : IClinicAppointmentScheduleRepository
    {
        private readonly DentalClinicBookingProjectContext dentalClinicBookingProjectContext;

        public ClinicAppointmentScheduleRepository(DentalClinicBookingProjectContext dentalClinicBookingProjectContext)
        {
            this.dentalClinicBookingProjectContext = dentalClinicBookingProjectContext;
        }

        public async Task<ClinicAppointmentSchedule> AddAsync(ClinicAppointmentSchedule clinicAppointmentSchedule)
        {            
                await dentalClinicBookingProjectContext.AddAsync(clinicAppointmentSchedule);
                await dentalClinicBookingProjectContext.SaveChangesAsync();
                return clinicAppointmentSchedule;            
        }

        public async Task<ClinicAppointmentSchedule?> GetAsync(Guid id)
        {
            return await dentalClinicBookingProjectContext.ClinicAppointmentSchedules
                .FirstOrDefaultAsync(x => x.ClinicAppointmentScheduleId == id);
        }

        public async Task<List<BookingSlot>> GetBookingsByDateAndClinicAsync(DateOnly date, string clinicName, string basicName)
        {
            return await dentalClinicBookingProjectContext.ClinicAppointmentSchedules
                   .Where(b => b.Date == date && b.ClinicName.Contains(clinicName) && b.BasicName.Contains(basicName))
                   .GroupBy(b => b.SlotName)
                   .Select(g => new BookingSlot { SlotName = g.Key, Count = g.Count() })
                   .ToListAsync();
        }
    }
}
