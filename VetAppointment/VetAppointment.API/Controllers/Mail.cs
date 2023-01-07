using Microsoft.AspNetCore.Mvc;
using VetAppointment.Infrastructure.Services;

namespace VetAppointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Mail : ControllerBase
    {
        private readonly IConfiguration configuration;
        public Mail(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        [HttpGet]
        public IActionResult Send()
        {
            var emailService = new CustomEmailService(this.configuration);
            string to = "mineamihai2001@gmai.com";
            string subject = "Appointment Created";
            string body = string.Format("<h1>Hi {0}</h1>. <p>An appointment was created for you. Please use the following link for payment.</p> <a target='_blank' href='http://localhost:3000/payment?id={1}'>Pay here</a>", "Testuser", "1234");
            emailService.Send(to, subject, body);
            return Ok();
        }
    }
}
