using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VetAppointment.API.DTOs.Authenticate;
using VetAppointment.Domain.Models;
using VetAppointment.Domain.Models.AuthenticationModels;
using VetAppointment.Infrastructure.Generics;
using VetAppointment.Infrastructure.Generics.GenericRepositories;

namespace VetAppointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private IConfiguration config;
        private readonly IRepository<User> userRepository;

        public UsersController(IConfiguration config, IRepository<User> userRepository)
        {
            this.config = config;
            this.userRepository = userRepository;
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
            var user = await userRepository.GetByUsernameAndPassword(dto.Username, dto.Password);

            if (user != null) {
                var token = Generate(user);
                return Ok(token);
            }

            return NotFound("User not found!");
        }

        [AllowAnonymous]
        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            //var verifyUser = userRepository.GetByUsername(dto.Username);
            //if(verifyUser != null)
            //{
            //    return Conflict("Username taken!");
            //}

            //verifyUser = userRepository.GetByEmail(dto.EmailAdress);
            //if(verifyUser != null)
            //{
            //    return Conflict("Email already used!");
            //}

            var user = new User(dto.Username, dto.Password, dto.Role, dto.EmailAdress, dto.FirstName, dto.LastName, dto.Phone);

            await userRepository.Add(user);
            await userRepository.SaveChanges();
            return Created(nameof(Get), user);
        }

        private string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.EmailAdress),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.MobilePhone, user.Phone)
            };

            var token = new JwtSecurityToken(config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
