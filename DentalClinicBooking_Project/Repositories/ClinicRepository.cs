using DentalClinicBooking_Project.Data;
using DentalClinicBooking_Project.Models;
using DentalClinicBooking_Project.Models.Domain;
using Microsoft.EntityFrameworkCore;


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
                .Include(x => x.Services)
                .FirstOrDefaultAsync(x => x.ClinicId == id);

        }


        public async Task<int> CountAsync()
        {
            return await dentalClinicBookingProjectContext.Clinics.CountAsync();
        }

        public async Task<int> CountAppointmentAsync()
        {
            var count = await dentalClinicBookingProjectContext.ClinicAppointmentSchedules
                            .GroupBy(a => new { a.ClinicName, a.BasicName, a.Day, a.SlotName })
                            .Select(g => new
                            {
                                g.Key.ClinicName,
                                g.Key.BasicName,
                                g.Key.Day,
                                g.Key.SlotName,
                                Count = g.Count()
                            }).SumAsync(x => x.Count);
            return count;
        }
    }
}
