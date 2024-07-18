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

        public async Task<ClinicAppointmentSchedule?> DeleteAsyn(Guid id)
        {
            var appointmentSchedule = await dentalClinicBookingProjectContext.ClinicAppointmentSchedules.FindAsync(id);

            if (appointmentSchedule != null)
            {
                dentalClinicBookingProjectContext.ClinicAppointmentSchedules.Remove(appointmentSchedule);
                await dentalClinicBookingProjectContext.SaveChangesAsync();
                return appointmentSchedule;
            }

            return null;
        }

        public async Task<IEnumerable<ClinicAppointmentSchedule>> GetAllAsync(Guid id)
        {
            return await dentalClinicBookingProjectContext.ClinicAppointmentSchedules
                .Include(x => x.Basic)
                .Include(x => x.Clinic)
                .Include(x => x.Service)
                .Include(x => x.Patient)
                .Where(x => x.PatientId == id)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClinicAppointmentSchedule>> SearchAsync(string? searchQuery, Guid id)
        {
            var query = dentalClinicBookingProjectContext.ClinicAppointmentSchedules.AsQueryable();
            if (string.IsNullOrWhiteSpace(searchQuery) == false)
            {
                query = query.Where(x => x.Code.Contains(searchQuery));
            }
            return await query
                .Include(x => x.Basic)
                .Include(x => x.Clinic)
                .Include(x => x.Service)
                .Include(x => x.Patient)
                .Where(x => x.PatientId == id)
                .ToListAsync();
        }

        public async Task<ClinicAppointmentSchedule?> GetAsync(Guid id)
        {
            return await dentalClinicBookingProjectContext.ClinicAppointmentSchedules
                .FirstOrDefaultAsync(x => x.ClinicAppointmentScheduleId == id);
        }
        public async Task<ClinicAppointmentSchedule?> GetAsync(string code)
        {
            return await dentalClinicBookingProjectContext.ClinicAppointmentSchedules
                .Include(x => x.Basic)
                .Include(x => x.Clinic)
                .Include(x => x.Service)
                .Include(x => x.Patient)
                .FirstOrDefaultAsync(x => x.Code.Equals(code));
        }
        public async Task<ClinicAppointmentSchedule?> GetDuplicateAsync(
            ClinicAppointmentSchedule clinicAppointmentSchedule)
        {
            return await dentalClinicBookingProjectContext.ClinicAppointmentSchedules
                   .FirstOrDefaultAsync(x => x.Date == clinicAppointmentSchedule.Date
                   && x.ClinicId == clinicAppointmentSchedule.ClinicId
                   && x.BasicId == clinicAppointmentSchedule.BasicId
                   && x.PatientId == clinicAppointmentSchedule.PatientId
                   && x.SlotId == clinicAppointmentSchedule.SlotId);
        }

        public async Task<List<BookingSlot>> GetSlotAsync(DateOnly date, Guid clinicId, Guid basicId)
        {
            return await dentalClinicBookingProjectContext.ClinicAppointmentSchedules
                   .Where(b => b.Date == date && b.ClinicId == clinicId && b.BasicId == basicId)
                   .GroupBy(b => b.SlotId)
                   .Select(g => new BookingSlot { SlotId = g.Key, Count = g.Count() })
                   .ToListAsync();
        }
    }
}
