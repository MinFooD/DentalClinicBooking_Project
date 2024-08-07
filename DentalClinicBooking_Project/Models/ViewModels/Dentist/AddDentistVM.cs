﻿using DentalClinicBooking_Project.Models.Domain;
using PayPal.Api;
using System.ComponentModel.DataAnnotations;

namespace DentalClinicBooking_Project.Models.ViewModels.Dentist
{
	public class AddDentistVM
	{
		[Required(ErrorMessage = "DentistName can not be blank.")]
		public string DentistName { get; set; }
		[Required(ErrorMessage = "Image can not be blank.")]
		public string Image { get; set; }
		[Required(ErrorMessage = "Experience can not be blank.")]
		public string Experience { get; set; }
		[Required(ErrorMessage = "Description can not be blank.")]
		public string Description { get; set; }
		[Required(ErrorMessage = "Gmail can not be blank.")]
		public string Gmail { get; set; }
		[Required(ErrorMessage = "UserName can not be blank.")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "Password can not be blank.")]
		public string Password { get; set; }
		[Required(ErrorMessage = "BasicID can not be blank.")]
		public Guid BasicId { get; set; }
		[Required(ErrorMessage = "Gender can not be blank.")]
		public bool Gender { get; set; }
		[RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
		public string Phone { get; set; }

		public List<Basic>? basics { get; set; }

	}
}
