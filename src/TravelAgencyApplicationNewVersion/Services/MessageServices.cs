using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TravelAgencyApplicationNewVersion.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {

        public void SendEmailAsync(string email, string subject, string message)
        {

            try
            {

                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("Economic Travel booking confirmation email", "sendconfirmation123@gmail.com"));

                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart("plain") { Text = message };

                using (var client = new SmtpClient())
                {
                    var credentials = new NetworkCredential
                    {
                        UserName = "sendconfirmation123@gmail.com",
                        Password = "afg982fs3"
                    };

                    client.Connect("smtp.gmail.com", 587, false);
                    //client.Authenticate(credentials);
                    client.Authenticate("sendconfirmation123@gmail.com", "afg982fs3");
                    //client.AuthenticateAsync(credentials);
                    client.Send(emailMessage);
                    client.Disconnect(true);

                    //      client.LocalDomain = "gmail.com";
                    //await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.Auto).ConfigureAwait(false);
                    //await client.AuthenticateAsync(credentials);
                    //await client.SendAsync(emailMessage).ConfigureAwait(false);
                    //await client.DisconnectAsync(true).ConfigureAwait(false);
                }
            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

            }
        }

            //    try { 

            //    var emailMessage = new MimeMessage();

            //    emailMessage.From.Add(new MailboxAddress("Joe Bloggs", "sendconfirmation123@gmail.com"));
            //    emailMessage.To.Add(new MailboxAddress("", email));
            //    emailMessage.Subject = subject;
            //    emailMessage.Body = new TextPart("plain") { Text = message };

            //    using (var client = new SmtpClient())
            //    {
            //            //System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("username", "password");
            //            //client.Credentials = credentials;
            //            client.Authenticate("sendconfirmation123@gmail.com", "afg982fs3");
            //            client.LocalDomain = "WORKGROUP";                    
            //        client.Connect("smtp.gmail.com", 587, SecureSocketOptions.None);
            //            try { 
            //        client.Send(emailMessage);
            //            }
            //            catch (Exception e)
            //            {

            //                Console.WriteLine(e.Message);

            //            }
            //            client.Disconnect(true);
            //    }

            //    }

            //    catch (Exception e)
            //    {

            //        Console.WriteLine(e.Message);                

            //    }

            //}


            //public async Task SendEmailAsync(string email, string subject, string message)
            //{
            //    var emailMessage = new MimeMessage();

            //    emailMessage.From.Add(new MailboxAddress("Joe Bloggs", "jbloggs@example.com"));
            //    emailMessage.To.Add(new MailboxAddress("", email));
            //    emailMessage.Subject = subject;
            //    emailMessage.Body = new TextPart("plain") { Text = message };

            //    using (var client = new SmtpClient())
            //    {
            //        client.LocalDomain = "some.domain.com";
            //        await client.ConnectAsync("smtp.relay.uri", 25, SecureSocketOptions.None).ConfigureAwait(false);
            //        await client.SendAsync(emailMessage).ConfigureAwait(false);
            //        await client.DisconnectAsync(true).ConfigureAwait(false);
            //    }
            //}

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }

        //Task IEmailSender.SendEmailAsync(string email, string subject, string message)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
