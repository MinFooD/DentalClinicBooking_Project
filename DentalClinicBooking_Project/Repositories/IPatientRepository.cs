using DentalClinicBooking_Project.Models.Domain;

namespace DentalClinicBooking_Project.Repositories
{
    public interface IPatientRepository
    {
        Task<Patient?> GetAsync(Guid id);

        bool SendMailGoogleSmtp(string _from, string _to, string _subject,
                                                  string _body, string _gmailsend, string _gmailpassword);

        string SendMailForm(string username, string password);
        
    }
}
