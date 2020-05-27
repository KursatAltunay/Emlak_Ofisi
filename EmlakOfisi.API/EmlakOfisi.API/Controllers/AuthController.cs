using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EmlakOfisi.API.Data;
using EmlakOfisi.API.Dtos;
using EmlakOfisi.API.Helper;
using EmlakOfisi.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EmlakOfisi.API.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthRepository _authRepository;
        IConfiguration _configuration;

        public AuthController(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]AgentForRegisterDto agentForRegisterDto)
        {
            if (await _authRepository.UserExists(agentForRegisterDto.Username))
            {
                ModelState.AddModelError("Username", "Username already exists");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var agentToCreate = new Agent
            {
                CompanyName = agentForRegisterDto.CompanyName,
                Username = agentForRegisterDto.Username
            };

            var createdAgent = await _authRepository.Register(agentToCreate, agentForRegisterDto.Password);
            return StatusCode(201);
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] AgentForLoginDto agentForLoginDto)
        {
            var agent = await _authRepository.Login(agentForLoginDto.Username, agentForLoginDto.Password);

            if (agent == null)
            {
                return Unauthorized();

            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier,agent.Id.ToString()), //token içinde neler tutulacağı belirlenir
                    new Claim(ClaimTypes.Name,agent.Username)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(new TokenModel(tokenString));

        }
    }
}