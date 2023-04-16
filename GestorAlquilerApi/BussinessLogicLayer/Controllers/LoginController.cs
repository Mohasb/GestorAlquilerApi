using System.Security.Claims;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using GestorAlquilerApi.DataAccessLayer.Data;
using GestorAlquilerApi.BussinessLogicLayer.DTOs;

namespace GestorAlquilerApi.BussinessLogicLayer.Controllers
{
    [Route("api/[controller]")]
    [Tags("!Login")]
    [ApiController]

    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private readonly ApiContext _context;
        private readonly IMapper _mapper;


        public LoginController(IConfiguration config, ApiContext context, IMapper mapper)
        {
            _config = config;
            _context = context;
            _mapper = mapper;
        }
        private ClientDTO? AuthenticateUser(UserDTO user)
        {

            var usuario = (from c in _context.Client where c.Email == user.Email select c).FirstOrDefault();

            if (usuario != null && BCrypt.Net.BCrypt.Verify(user.Password, usuario.Password))
            {
                return _mapper.Map<ClientDTO>(usuario);
            }
            return null;


        }
        private string GenerateToken(ClientDTO user)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? ""));
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
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(UserDTO user)
        {
            IActionResult response = Unauthorized();
            var user_ = AuthenticateUser(user);

            if (user_ != null)
            {
                var token = GenerateToken(user_);
                response = Ok(new { token });
                return response;
            }
            return NotFound("Usuario NO encontrado");
        }
    }

}
