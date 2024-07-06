using DentalClinicBooking_Project.Data;
using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels.BookingClinicModels;
using Microsoft.EntityFrameworkCore;

namespace DentalClinicBooking_Project.Repositories
{
    public class SlotRepository : ISlotRepository
    {
        private readonly DentalClinicBookingProjectContext dentalClinicBookingProjectContext;

        public SlotRepository(DentalClinicBookingProjectContext dentalClinicBookingProjectContext)
        {
            this.dentalClinicBookingProjectContext = dentalClinicBookingProjectContext;
        }
        public async Task<SlotOfClinic[]> GetAllSlotsAsync()
        {
            return await dentalClinicBookingProjectContext.SlotOfClinics
                .GroupBy(s => s.SlotId)
                .Select(g => g.FirstOrDefault()!)
                .ToArrayAsync();
        }
    }
}
