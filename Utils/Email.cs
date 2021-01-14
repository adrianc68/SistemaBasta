using System.Net.Mail;
using System.Net;
using System;
using Utils.Properties.Configuration;

namespace EmailSender {
    /*
        The Email class
        Contains all methods for send a message by smtp
    */
    public class Email {
        // Send a message to specified email
        /// <summary>
        /// It sends a message by smtp with content.
        /// </summary>
        /// <exception cref="Exception"> Thrown when an exception occurs.</exception>
        public static void SendMessage( string emailToSend, string content ) {
            string FROM = Resource.From_Email;
            string FROMNAME = Resource.From_Email_Name;
            string TO = emailToSend;
            string SMTP_USERNAME = Resource.From_Email;
            string SMTP_PASSWORD = Resource.SMPT_password;
            string HOST = Resource.Host;
            int PORT = Int16.Parse( Resource.Port );
            string SUBJECT = Resource.SMPT_Subject;
            string BODY = content;
            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress( FROM, FROMNAME );
            message.To.Add( new MailAddress( TO ) );
            message.Subject = SUBJECT;
            message.Body = BODY;
            using ( var client = new System.Net.Mail.SmtpClient( HOST, PORT ) ) {
                client.Credentials = new NetworkCredential( SMTP_USERNAME, SMTP_PASSWORD );
                client.EnableSsl = true;
                try {
                    client.Send(message);
                } catch ( Exception) {
                    throw;
                }
            }
        }

    }
}
