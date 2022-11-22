using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetAppointment.Domain.Models;
using VetAppointment.Infrastructure.Generics.GenericRepositories;

namespace VetAppointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugController : ControllerBase
    {
        private readonly IDrugRepository drugRepository;

        public DrugController(IDrugRepository drugRepository)
        {
            this.drugRepository = drugRepository;
        }


        [HttpGet]
        public ActionResult Get(int id)
        {
            return Ok(drugRepository.GetAll());
        }

        [HttpPost]
        public ActionResult Create()
        {
            var drug = new Drug("name", 2, 10, false);
            drugRepository.Add(drug);
            drugRepository.Save();
            return Created(nameof(Get), drug);
        }
    }
}
