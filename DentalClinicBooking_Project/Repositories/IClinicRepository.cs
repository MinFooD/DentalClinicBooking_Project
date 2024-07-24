using Azure;
using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels.BookingClinicModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DentalClinicBooking_Project.Repositories
{
    public interface IClinicRepository
    {
        Task<IEnumerable<Clinic>> GetAllAsync(
            string? searchQuery = null,
            int pageNumber = 1,
            int pageSize = 100);

        Task<IEnumerable<Clinic>> GetAllForAdminAsync();

        Task<Clinic?> GetAsync(Guid id);

        Task<int?> CountAsync(string? searchQuery);
        Clinic? UpdateStatus(Guid id);
        Clinic Delete(Guid id);
    }
}
