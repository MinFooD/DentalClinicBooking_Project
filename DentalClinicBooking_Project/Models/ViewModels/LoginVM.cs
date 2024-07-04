using System.ComponentModel.DataAnnotations;

namespace DentalClinicBooking_Project.Models.ViewModels
{
	public class LoginVM
	{
		[Required(ErrorMessage ="Gmail can not be blank.")]
		public string Gmail { get; set; }
		[Required(ErrorMessage = "Password can not be blank.")]
		public string Password { get; set; }
		[Required]
		[Compare("Password")]
		public int RoleId { get; set; }
		[Required(ErrorMessage = "PatientName can not be blank.")]
		public string PatientName { get; set; }
		[Required(ErrorMessage = "Phone can not be blank.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string Phone { get; set; }
		[Required(ErrorMessage = "BirthDay can not be blank.")]
		public DateOnly BirthDay { get; set; }
		[Required(ErrorMessage = "Gender can not be blank.")]
		public bool? Gender { get; set; }
		[Required(ErrorMessage = "Address can not be blank.")]
		public string Address { get; set; }

		public string CitizenIdentificationCard { get; set; }

		public string Nation { get; set; }

		public string Job { get; set; }

		public string HealthInsuranceCardCode { get; set; }


	}
}
