using System.Net.Mail;
using System.Net;

namespace Authentication.Services.Operations
{
    public class Email
    {
        public Email()
        {

        }


        public void SendEmail(string smtpServer, string email, string password,
                            string to, string subject, string body)
        {

            SmtpClient SmtpClient = new SmtpClient(smtpServer)
            {
                Port = 587,
                Credentials = new NetworkCredential(email, password),
               // EnableSsl = true,
            };

            SmtpClient.Send(email, to, subject, body);

        }

    }
}
