using DentalClinicBooking_Project.Models.Domain;

namespace DentalClinicBooking_Project.Repositories
{
    public interface IBasicRepository
    {
        Task<Basic?> GetAsync(Guid id);
	}
}
