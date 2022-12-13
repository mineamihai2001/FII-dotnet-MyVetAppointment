using AutoMapper;
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
        private readonly IMapper mapper;

        public BillsController(IRepository<Bill> billRepository, IMapper mapper)
        {
            this.billRepository=billRepository;
            this.mapper=mapper;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(billRepository.GetAll().Result);
        }

    }
}
