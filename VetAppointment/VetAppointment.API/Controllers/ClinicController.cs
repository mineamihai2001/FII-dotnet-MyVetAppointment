using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetAppointment.Domain.Models;
using VetAppointment.Infrastructure.Generics.GenericRepositories;
using VetAppointment.Infrastructure.Repositories;

namespace VetAppointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        private readonly IClinicRepository clinicRepository;

        public ClinicController(IClinicRepository clinicRepository)
        {
            this.clinicRepository = clinicRepository;
        }


        [HttpGet]
        public ActionResult Get(int id)
        {
            return Ok(clinicRepository.GetAll());
        }

        [HttpPost]
        public ActionResult Create()
        {
            var clinic = new Clinic();
            clinicRepository.Add(clinic);
            clinicRepository.Save();
            return Created(nameof(Get), clinic);
        }
    }
}
