using DentalClinicBooking_Project.Models.Domain;

namespace DentalClinicBooking_Project.Repositories
{
    public interface IServiceRepository
    {
        Task<Service?> GetAsync(Guid id);
    }
}
