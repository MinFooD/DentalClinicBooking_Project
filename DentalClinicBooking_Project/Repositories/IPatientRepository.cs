using DentalClinicBooking_Project.Models.Domain;

namespace DentalClinicBooking_Project.Repositories
{
    public interface IPatientRepository
    {
        Task<Patient?> GetAsync(Guid id);
    }
}
