using Azure;
using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels.BookingClinicModels;

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
        Task<List<BookingInfo>> GetBookingsByDateAndClinic(
            DateOnly date,
            string clinicName,
            string basicName);
    }
}
