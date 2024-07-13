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

        public SlotOfClinic? Get(Guid clinicId, Guid slotId)
        {
            return  dentalClinicBookingProjectContext.SlotOfClinics
        .FirstOrDefault(x => x.ClinicId == clinicId && x.SlotId == slotId);
        }

        public async Task<Slot[]> GetAllSlotsAsync()
        {
            return await dentalClinicBookingProjectContext.Slots
                .GroupBy(s => s.SlotId)
                .Select(g => g.FirstOrDefault()!)
                .ToArrayAsync();
        }

        public async Task<SlotOfClinic?> GetAsync(Guid clinicId, Guid slotId)
        {
            return await dentalClinicBookingProjectContext.SlotOfClinics
        .FirstOrDefaultAsync(x => x.ClinicId == clinicId && x.SlotId == slotId);

        }
    }
}
