using DentalClinicBooking_Project.Data;
using DentalClinicBooking_Project.Models;
using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels.BookingClinicModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace DentalClinicBooking_Project.Repositories
{
    public class ClinicRepository : IClinicRepository
    {
        private readonly DentalClinicBookingProjectContext dentalClinicBookingProjectContext;

        public ClinicRepository(DentalClinicBookingProjectContext dentalClinicBookingProjectContext)
        {
            this.dentalClinicBookingProjectContext = dentalClinicBookingProjectContext;
        }

        public async Task<IEnumerable<Clinic>> GetAllAsync(
            string? searchQuery,
            int pageNumber = 1,
            int pageSize = 100)
        {
            var query = dentalClinicBookingProjectContext.Clinics.AsQueryable();

            if (string.IsNullOrWhiteSpace(searchQuery) == false)
            {
                query = query.Where(x => x.ClinicName.Contains(searchQuery));
            }

            var skipResults = (pageNumber - 1) * pageSize;
            query = query.Skip(skipResults).Take(pageSize);

            return await query.Include(x => x.Basics).ToListAsync();
        }

        public async Task<Clinic?> GetAsync(Guid id)
        {

            return await dentalClinicBookingProjectContext.Clinics
                .Include(x => x.Basics)
                .Include(x => x.ClinicImages)
                .Include(x => x.SlotOfClinics)
                .ThenInclude(x => x.Slot)
                .Include(x => x.Services)
                .FirstOrDefaultAsync(x => x.ClinicId == id);

        }


        public async Task<int> CountAsync()
        {
            return await dentalClinicBookingProjectContext.Clinics.CountAsync();
        }


        public async Task<List<BookingSlot>> GetBookingsByDateAndClinicAsync(DateOnly date, string clinicName, string basicName)
        {
            return await dentalClinicBookingProjectContext.ClinicAppointmentSchedules
                   .Where(b => b.Date == date && b.ClinicName.Contains(clinicName) && b.BasicName.Contains(basicName))
                   .GroupBy(b => b.SlotName)
                   .Select(g => new BookingSlot { SlotName = g.Key, Count = g.Count() })
                   .ToListAsync();
        }

        public async Task<ClinicAppointmentSchedule> AddAsync(ClinicAppointmentSchedule clinicAppointmentSchedule)
        {
            await dentalClinicBookingProjectContext.AddAsync(clinicAppointmentSchedule);
            await dentalClinicBookingProjectContext.SaveChangesAsync();
            return clinicAppointmentSchedule;
        }

        public async Task<SlotOfClinic[]> GetAllSlotsAsync()
        {
            return await dentalClinicBookingProjectContext.SlotOfClinics
                .GroupBy(s => new { s.SlotId, s.StartTime, s.EndTime })
                .Select(g => g.FirstOrDefault()!)
                .ToArrayAsync();

            //return await dentalClinicBookingProjectContext.SlotOfClinics
            //    .GroupBy(s => new { s.SlotId, s.StartTime, s.EndTime })
            //    .Select(g => new BookingTime{ 
            //        SlotId = g.,
            //        StartTime = g.s=
            //    })

        }
    }
}
