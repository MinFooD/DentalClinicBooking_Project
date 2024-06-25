namespace DentalClinicBooking_Project.Models.ViewModels
{
    public class SearchDentist()
    {
        public string SearchString { get; set; }
        public List<DentistWithClinicName> dentists { get; set; }
        public int ResultCount { get; set; }
    }
}
