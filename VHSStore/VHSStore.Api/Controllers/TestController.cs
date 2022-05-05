using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using System;
using System.Threading.Tasks;
using System.Transactions;
using VHSStore.Application.Interfaces;
using VHSStore.Domain.Models.GenreModels;

namespace VHSStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail()
        {
            try
            {
                var messageToSend = new MimeMessage()
                { 
                    Sender = new MailboxAddress("Max", "mershaughnessy@gmail.com"),
                    Subject = "Test"
                };

                messageToSend.From.Add(new MailboxAddress("Max", "mershaughnessy@gmail.com"));
                messageToSend.Body = new TextPart(TextFormat.Plain) { Text = "This is test data."};
                messageToSend.To.Add(new MailboxAddress("Max", "mershaughnessy@gmail.com"));

                using (var smtp = new SmtpClient())
                {
                    smtp.MessageSent += (sender, args) => { System.Diagnostics.Debug.WriteLine(args.Response); };
                    smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    await smtp.ConnectAsync("smtp-relay.gmail.com", 587, SecureSocketOptions.StartTls);
                    await smtp.SendAsync(messageToSend);
                    await smtp.DisconnectAsync(true);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
