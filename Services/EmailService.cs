using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace CarRentalSystem.Services
{
    public class EmailService
    {
        private readonly string _sendGridApiKey;

        public EmailService(string sendGridApiKey)
        {
            _sendGridApiKey = sendGridApiKey;
        }

        public async Task SendCarRentalConfirmationEmail(string userEmail, string userName, string carMake, string carModel)
        {
            int rentalDuration = 5;
            var client = new SendGridClient(_sendGridApiKey);
            var from = new EmailAddress("prakharkr.singh14365@gmail.com", "Car Rental System");
            var to = new EmailAddress(userEmail,userName);
            var subject = "Car Rental Confirmation";
            var plainTextContent = $"Dear {userName},\n\nYou have successfully rented a {carMake} {carModel} for {rentalDuration} days.\n\nThank you for using our service!";
            var htmlContent = $"<strong>Dear {userName},</strong><br><br>You have successfully rented a <strong>{carMake} {carModel}</strong> for <strong>{rentalDuration}</strong> days.<br><br>Thank you for using our service!";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);
            


         
        }
    }
}
