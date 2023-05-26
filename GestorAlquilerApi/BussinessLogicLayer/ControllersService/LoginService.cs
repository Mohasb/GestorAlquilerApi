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
            var _user = CheckUserEmailPassword(user);

            if (_user != null)
            {
                var token = GenerateToken(_user);

                string firstDigits = _user.BankAccount!.Substring(0, _user.BankAccount.Length - 4);
                string asteriscos = new String('*', firstDigits.Length);

                string lastFourDigits = _user.BankAccount!.Substring(_user.BankAccount.Length - 4);

                var userWithToken = new
                {
                    id = _user.Id,
                    registration = _user.Registration,
                    rol = _user.Rol,
                    email = _user.Email,
                    name = _user.Name,
                    lastName = _user.LastName,
                    phoneNumber = _user.PhoneNumber,
                    bankAccount = asteriscos + lastFourDigits,
                    token = token,
                    image = _user.Image
                };
                return new JsonResult(
                    new
                    {
                        statusCode = (int)HttpStatusCode.OK,
                        isOk = true,
                        userWithToken
                    }
                );
            }

            var usuario = _repository.GetClientByEmail(user);
            if (usuario == null)
            {
                return new JsonResult(
                    new
                    {
                        statusCode = (int)HttpStatusCode.NotFound,
                        isOk = false,
                        responseText = "Usuario no encontrado"
                    }
                );
            }
            else if (!BCrypt.Net.BCrypt.Verify(user.Password, usuario.Password))
            {
                return new JsonResult(
                    new
                    {
                        statusCode = (int)HttpStatusCode.NotFound,
                        isOk = false,
                        responseText = "El password no es correcto"
                    }
                );
            }
            else
            {
                return new JsonResult(
                    new
                    {
                        statusCode = (int)HttpStatusCode.NotFound,
                        isOk = false,
                        responseText = "Error Login"
                    }
                );
            }
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
