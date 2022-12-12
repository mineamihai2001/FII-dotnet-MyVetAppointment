using Microsoft.AspNetCore.Mvc;
using VetAppointment.API.DTOs;
using VetAppointment.Domain.Models;
using VetAppointment.Infrastructure.Generics;
using VetAppointment.Infrastructure.Generics.GenericRepositories;

namespace VetAppointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : Controller
    {
        private readonly IRepository<Bill> billRepository;

        public BillsController(IRepository<Bill> billRepository)
        {
            this.billRepository=billRepository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(billRepository.GetAll().Result);
        }

    }
}
