using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VetAppointment.API.DTOs.Authenticate;
using VetAppointment.API.Helpers;
using VetAppointment.Domain.Models;
using VetAppointment.Domain.Models.AuthenticationModels;
using VetAppointment.Infrastructure.Generics;
using VetAppointment.Infrastructure.Services;
using VetAppointment.Infrastructure.Generics.GenericFilters;
using VetAppointment.Infrastructure.Generics.GenericRepositories;

namespace VetAppointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Medic> medicRepository;
        private readonly JwtService jwtService;

        public UsersController(IConfiguration configuration, IRepository<User> userRepository,
            IRepository<Medic> medicRepository)
        {
            this.configuration = configuration;
            this.userRepository = userRepository;
            this.medicRepository = medicRepository;
            this.jwtService = new JwtService(configuration);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await userRepository.GetAll());
        }

        [AllowAnonymous]
        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            var user = userRepository
                .GetAll()
                .Result!
                .FirstOrDefault(u => u.EmailAddress == dto.EmailAddress);

            if (user == null)
                return Ok(new Response(ResponseStatus.Error, "Invalid username"));
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                return Ok(new Response(ResponseStatus.Error, "Invalid password"));

            var token = jwtService.Generate(user);
            var responseDto = new ResponseLoginDto(ResponseStatus.Success, token, user.Id, user.MedicId);
            return Ok(responseDto);
        }

        [AllowAnonymous]
        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            var exists = userRepository
                .GetAll()
                .Result!
                .FirstOrDefault(u => u.EmailAddress == dto.EmailAddress);

            if (exists != null) return Ok(new Response(ResponseStatus.Error, "Email already taken"));

            var medic = new Medic("", "", dto.EmailAddress);
            await medicRepository.Add(medic);
            await medicRepository.SaveChanges();

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var user = new User(dto.EmailAddress, hashedPassword, dto.Role, medic.Id);

            await userRepository.Add(user);
            await userRepository.SaveChanges();
            return Created(nameof(Get), user);
        }
    }
}