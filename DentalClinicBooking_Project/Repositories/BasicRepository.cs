using DentalClinicBooking_Project.Data;
using DentalClinicBooking_Project.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DentalClinicBooking_Project.Repositories
{
    public class BasicRepository : IBasicRepository
    {
        private readonly DentalClinicBookingProjectContext dentalClinicBookingProjectContext;

        public BasicRepository(DentalClinicBookingProjectContext dentalClinicBookingProjectContext)
        {
            this.dentalClinicBookingProjectContext = dentalClinicBookingProjectContext;
        }

        public async Task<Basic?> GetAsync(Guid id)
        {
            return await dentalClinicBookingProjectContext.Basics
                .Include(x => x.Clinic)
                .ThenInclude(x => x.SlotOfClinics)
                .FirstOrDefaultAsync(x => x.BasicId == id);
        }
    }
}
