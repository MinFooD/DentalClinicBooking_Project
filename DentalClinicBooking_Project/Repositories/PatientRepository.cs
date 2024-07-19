using DentalClinicBooking_Project.Data;
using DentalClinicBooking_Project.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;

namespace DentalClinicBooking_Project.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly DentalClinicBookingProjectContext dentalClinicBookingProjectContext;

        public PatientRepository(DentalClinicBookingProjectContext dentalClinicBookingProjectContext)
        {
            this.dentalClinicBookingProjectContext = dentalClinicBookingProjectContext;
        }

        public async Task<Patient?> GetAsync(Guid id)
        {
            return await dentalClinicBookingProjectContext.Patients
            .Include(x => x.ClinicAppointmentSchedules)
            .FirstOrDefaultAsync(x => x.PatientId == id);
        }

        //hàm của Food: gửi password về gmail.
        public bool SendMailGoogleSmtp(string _from, string _to, string _subject,
                                                  string _body, string _gmailsend, string _gmailpassword)
        {

            MailMessage message = new MailMessage(
                from: _from,
                to: _to,
                subject: _subject,
                body: _body
            );
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;

            // Tạo SmtpClient kết nối đến smtp.gmail.com
            using (SmtpClient client = new SmtpClient("smtp.gmail.com"))
            {
                client.UseDefaultCredentials = false;
                client.Port = 587;
                client.Credentials = new NetworkCredential(_gmailsend, _gmailpassword);
                client.EnableSsl = true;

                client.Send(message);
            }
            return true;
        }
        //hàm của Food: định dạng form gửi về mail
        public string SendMailForm(string username, string password)
        {
            return $@"
            <!DOCTYPE html>
            <html>
            <head>
                <style>
                    .container {{
                        width: 90%;
                        padding: 20px;
                        background-color: #f4f4f4;
                        font-family: Arial, sans-serif;
                    }}
                    .content {{
                        background-color: #ffffff;
                        padding: 20px;
                        border-radius: 10px;
                        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                    }}
                    .header {{
                        font-size: 24px;
                        font-weight: bold;
                        color: #333333;
                        margin-bottom: 20px;
                        text-align: center;
                    }}
                    .message {{
                        font-size: 16px;
                        color: #555555;
                        line-height: 1.5;
                    }}
                    .footer {{
                        margin-top: 20px;
                        font-size: 14px;
                        color: #999999;
                    }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <div class='content'>
                        <div class='header'>Reset Password Request</div>
                        <div class='message'>
                            Dear {username},<br><br>
                            We have received a request to reset your password. Your password is:<br><br>
                            <strong>{password}</strong><br><br>
                            Please use this password to log in to your account and ensure to change it after logging in for security reasons.<br><br>
                            If you did not request a password reset, please ignore this email or contact our support.<br><br>
                            Best regards,<br>
                            SunShine
                        </div>
                        <div class='footer'>
                            This email was sent from a notification-only address that cannot accept incoming email. Please do not reply to this message.
                        </div>
                    </div>
                </div>
            </body>
            </html>";
        }
    }
}
