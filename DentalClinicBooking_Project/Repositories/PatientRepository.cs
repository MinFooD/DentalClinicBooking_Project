using DentalClinicBooking_Project.Data;
using DentalClinicBooking_Project.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DentalClinicBooking_Project.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly DentalClinicBookingProjectContext dentalClinicBookingProjectContext;

        public PatientRepository(DentalClinicBookingProjectContext dentalClinicBookingProjectContext)
        {
            this.dentalClinicBookingProjectContext = dentalClinicBookingProjectContext;
        }

        public async Task<Patient?> GetAsync(Guid id)
        {
            return await dentalClinicBookingProjectContext.Patients
            .Include(x => x.ClinicAppointmentSchedules)
            .FirstOrDefaultAsync(x => x.PatientId == id);
        }
    }
}
