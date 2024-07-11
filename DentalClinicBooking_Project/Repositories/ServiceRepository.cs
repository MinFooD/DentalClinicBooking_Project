using DentalClinicBooking_Project.Data;
using DentalClinicBooking_Project.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DentalClinicBooking_Project.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly DentalClinicBookingProjectContext dentalClinicBookingProjectContext;

        public ServiceRepository(DentalClinicBookingProjectContext dentalClinicBookingProjectContext)
        {
            this.dentalClinicBookingProjectContext = dentalClinicBookingProjectContext;
        }
        public async Task<Service?> GetAsync(Guid id)
        {
            return await dentalClinicBookingProjectContext.Services
             .FirstOrDefaultAsync(x => x.ServiceId == id);
        }
    }
}
