using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GestorAlquilerApi.BussinessLogicLayer.ControllersService
{
    public class LoginService : ILoginService
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IQueryClient _repository;

        public LoginService(IConfiguration config, IQueryClient repository, IMapper mapper)
        {
            _config = config;
            _repository = repository;
            _mapper = mapper;
        }

        public ClientDTO? AuthenticateUser(UserDTO user)
        {
            var usuario = _repository.GetClientByEmail(user);

            if (usuario != null && BCrypt.Net.BCrypt.Verify(user.Password, usuario.Password))
            {
                return _mapper.Map<ClientDTO>(usuario);
            }
            return null;
        }

        public IActionResult Login(UserDTO user)
        {
            //IActionResult response;
            var _user = CheckUserEmailPassword(user);

            if (_user != null)
            {
                var token = GenerateToken(_user);

                //Response.StatusCode = (int)HttpStatusCode.OK;
                return new JsonResult(
                    new
                    {
                        statusCode = (int)HttpStatusCode.OK,
                        isOk = true,
                        token,
                        _user.Rol
                    }
                );
            }
            return new JsonResult(
                new
                {
                    statusCode = (int)HttpStatusCode.NotFound,
                    isOk = false,
                    responseText = "NOT OK"
                }
            );
        }

        public ClientDTO? CheckUserEmailPassword(UserDTO user) => AuthenticateUser(user);

        public string GenerateToken(ClientDTO user)
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
    }
}
