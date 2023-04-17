using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;

namespace GestorAlquilerApi.BussinessLogicLayer.Controllers
{
    [Route("api/[controller]")]
    [Tags("!Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private IClientService _clientService;

        public LoginController(IConfiguration config, IClientService clientService)
        {
            _config = config;
            _clientService = clientService;
        }

        private ClientDTO? AuthenticateUser(UserDTO user) => _clientService.AuthenticateUser(user);

        private string GenerateToken(ClientDTO user)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? "")
            );
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Name ?? ""),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim(ClaimTypes.Role, user.Rol ?? ""),
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(UserDTO user)
        {
            IActionResult response = Unauthorized();
            var _user = AuthenticateUser(user);

            if (_user != null)
            {
                var token = GenerateToken(_user);
                response = Ok(new { token });
                return response;
            }
            return NotFound("Usuario NO encontrado");
        }
    }
}
