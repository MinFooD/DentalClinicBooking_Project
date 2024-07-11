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
        public async Task<Slot[]> GetAllSlotsAsync()
        {
            return await dentalClinicBookingProjectContext.Slots
                .GroupBy(s => s.SlotId)
                .Select(g => g.FirstOrDefault()!)
                .ToArrayAsync();
        }

        public async Task<IEnumerable<SlotOfClinic>> GetAsync(Guid clinicId, Guid slotId)
        {
            return await dentalClinicBookingProjectContext.SlotOfClinics
                .Include(x => x.Clinic)
                .Include(x => x.Slot)
                .Where(x => x.ClinicId == clinicId && x.SlotId == slotId)
                .ToListAsync();               
        }
    }
}
