using Azure;
using DentalClinicBooking_Project.Models.Domain;

namespace DentalClinicBooking_Project.Repositories
{
    public interface IClinicRepository
    {
        Task<IEnumerable<Clinic>> GetAllAsync(
            string? searchQuery = null,
            int pageNumber = 1,
            int pageSize = 100);

        Task<Clinic?> GetAsync(Guid id);

        Task<int> CountAsync();
    }
}
