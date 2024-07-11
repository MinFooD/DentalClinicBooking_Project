﻿using DentalClinicBooking_Project.Data;
using DentalClinicBooking_Project.Models.Domain;
using DentalClinicBooking_Project.Models.ViewModels.BookingClinicModels;

namespace DentalClinicBooking_Project.Repositories
{
    public interface ISlotRepository
    {
        Task<Slot[]> GetAllSlotsAsync();

        Task<IEnumerable<SlotOfClinic>> GetAsync(Guid clinicId, Guid slotId);       
    }
}