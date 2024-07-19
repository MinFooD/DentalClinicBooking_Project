using System.ComponentModel.DataAnnotations;

namespace DentalClinicBooking_Project.Models.ViewModels.PatientViewModel
{
    public class ResetPassword
    {
        [Required(ErrorMessage = "UserName can not be blank.")]
        public string UserName { get; set; }
        [EmailAddress(ErrorMessage = "Gmail can not be blank.")]
        public string Gmail { get; set; }
    }
}
