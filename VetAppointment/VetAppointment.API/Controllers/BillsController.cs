using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using VetAppointment.API.DTOs;
using VetAppointment.API.DTOs.Create;
using VetAppointment.Domain.Models;
using VetAppointment.Domain.Validators;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBillDto dto)
        {
            var bill = new Bill(dto.BillingDate, dto.Summary, dto.PaymentSum, dto.AppointmentId);
            var validator = new BillValidator();
            ValidationResult results = validator.Validate(bill);
            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
                return BadRequest(results.Errors);
            }
            await billRepository.Add(bill);
            await billRepository.SaveChanges();
            return Created(nameof(Get), bill);
        }
    }
}
