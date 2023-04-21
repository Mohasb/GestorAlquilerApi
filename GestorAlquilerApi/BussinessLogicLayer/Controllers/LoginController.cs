using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestorAlquilerApi.BussinessLogicLayer.Controllers
{
    [Route("api/[controller]")]
    [Tags("!Login")]
    [ApiController]
    public class LoginController
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult LoginUser(UserDTO user)
        {
            return _loginService.Login(user);
        }
    }
}
